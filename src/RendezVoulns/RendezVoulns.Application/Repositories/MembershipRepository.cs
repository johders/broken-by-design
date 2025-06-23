using Dapper;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Models.Enums;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.ReadModels;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class MembershipRepository(IDbConnectionFactory dbConnectionFactory) : IMembershipRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> JoinAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            INSERT INTO memberships (user_id, group_id, role, joined_on)
            VALUES (@UserId, @GroupId, @Role, @JoinedOn)
            ON CONFLICT (user_id, group_id) DO UPDATE
            SET role = EXCLUDED.role, joined_on = EXCLUDED.joined_on, updated_on = NOW(), deleted_on = NULL
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { UserId = userId, GroupId = groupId, Role = GroupRole.Member.ToString(), JoinedOn = DateTimeOffset.UtcNow }));

        transaction.Commit();
        return result > 0;
    }

    public async Task<Membership?> GetByIdAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT user_id AS UserId, group_id AS GroupId, role, joined_on AS JoinedOn, updated_on AS UpdatedOn FROM memberships
            WHERE group_id = @GroupId
            AND user_id = @UserId
            AND deleted_on IS NULL
            """;
        var memberships = await connection.QueryFirstOrDefaultAsync<Membership>(new CommandDefinition(sql, new { GroupId = groupId, UserId = userId }, cancellationToken: token));

        return memberships;
    }

    public async Task<IEnumerable<MembershipWithGroup>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT m.user_id AS UserId, m.group_id AS GroupId, m.role, m.joined_on AS JoinedOn, g.id AS Id, g.name, g.description
            FROM memberships m
            JOIN groups g ON g.id = m.group_id
            WHERE m.deleted_on IS NULL
            AND g.deleted_on IS NULL
            AND m.user_id = @UserId
            """;

    var command = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: token);

    var memberships = await connection.QueryAsync<MembershipWithGroup, GroupSummary, MembershipWithGroup>(
        command, (membership, groupSummary) =>
            {
                membership.Group = groupSummary;
                return membership;
            },
        splitOn: "Id"
    );

    return memberships;
    }

    public async Task<bool> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE memberships 
            SET updated_on = NOW(), deleted_on = NOW()
            WHERE group_id = @GroupId
            AND user_id = @UserId;
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { GroupId = groupId, UserId = userId }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Membership membership, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE memberships 
            SET role = @Role, updated_on = NOW()
            WHERE group_id = @GroupId
            AND user_id = @UserId;
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new { membership.GroupId, membership.UserId, Role = membership.Role.ToString() }, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }
}
