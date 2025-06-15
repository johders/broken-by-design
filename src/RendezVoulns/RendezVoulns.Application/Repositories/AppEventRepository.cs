using Dapper;
using Npgsql;
using RendezVoulns.Application.Errors.Postgresql;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class AppEventRepository(IDbConnectionFactory dbConnectionFactory) : IAppEventRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private const string UniqueViolationErrorCode = "23505";
    private const string ForeignKeyViolationErrorCode = "23503";

    private readonly List<AppEvent> _appEvents = [];
    public async Task<bool> CreateAsync(AppEvent appEvent, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            var sql = """
                INSERT INTO events (id, group_id, title, slug, description, location, start_time, end_time, created_by_user_id, created_on)
                VALUES (@Id, @GroupId, @Title, @Slug, @Description, @Location, @StartTime, @EndTime, @CreatedByUserId, @CreatedOn)
                """;

            var result = await connection.ExecuteAsync(new CommandDefinition(sql, appEvent, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == UniqueViolationErrorCode)
        {
            throw new DuplicateSlugException("An event with this slug already exists");
        }
        catch (PostgresException ex) when (ex.SqlState == ForeignKeyViolationErrorCode)
        {
            throw new ForeignKeyViolationException("Invalid group/user reference");
        }
    }

    public async Task<AppEvent?> GetByIdAsync(Guid id, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT id, group_id AS GroupId, title, slug, description, location, start_time AS StartTime, end_time AS EndTime, created_by_user_id AS CreatedByUserId, created_on AS CreatedOn FROM events
                WHERE id = @Id
                AND deleted_on IS NULL;
                """;

        var appEvent = await connection.QueryFirstOrDefaultAsync<AppEvent>(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        return appEvent;
    }

    public async Task<AppEvent?> GetBySlugAsync(string slug, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT id, group_id AS GroupId, title, slug, description, location, start_time AS StartTime, end_time AS EndTime, created_by_user_id AS CreatedByUserId, created_on AS CreatedOn FROM events
                WHERE slug = @Slug
                AND deleted_on IS NULL;
                """;

        var appEvent = await connection.QueryFirstOrDefaultAsync<AppEvent>(new CommandDefinition(sql, new { Slug = slug }, cancellationToken: token));

        return appEvent;
    }

    public async Task<IEnumerable<AppEvent>> GetAllAsync(CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT id, group_id AS GroupId, title, slug, description, location, start_time AS StartTime, end_time AS EndTime, created_by_user_id AS CreatedByUserId, created_on AS CreatedOn FROM events
                WHERE deleted_on IS NULL;
                """;

        var result = await connection.QueryAsync(new CommandDefinition(sql, cancellationToken: token));

        return result.Select(e => new AppEvent
        {
            Id = e.id,
            GroupId = e.groupid,
            Title = e.title,
            Description = e.description,
            Location = e.location,
            StartTime = e.starttime,
            EndTime = e.endtime,
            CreatedByUserId = e.createdbyuserid,
            CreatedOn = e.createdon
        });
    }


    public Task<bool> UpdateAsync(AppEvent appEvent)
    {
        var appEventIndex = _appEvents.FindIndex(e => e.Id == appEvent.Id);

        if (appEventIndex == -1)
        {
            return Task.FromResult(false);
        }

        _appEvents[appEventIndex] = appEvent;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var removedCount = _appEvents.RemoveAll(e => e.Id == id);
        var appEventRemoved = removedCount > 0;

        return Task.FromResult(appEventRemoved);
    }

    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}