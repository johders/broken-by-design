using Dapper;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Models.Enums;
using RendezVoulns.Application.Persistence.Database;
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

    public Task<Membership?> GetByIdAsync(Guid id, Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Membership>> GetUserMembershipsAsync(Guid userId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid userId, Guid groupId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Membership membership, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
