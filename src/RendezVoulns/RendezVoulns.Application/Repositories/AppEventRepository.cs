using Dapper;
using Npgsql;
using RendezVoulns.Application.Common.Errors;
using RendezVoulns.Application.Common.Exceptions;
using RendezVoulns.Application.Models.Entities;
using RendezVoulns.Application.Persistence.Database;
using RendezVoulns.Application.Repositories.Interfaces;

namespace RendezVoulns.Application.Repositories;

public class AppEventRepository(IDbConnectionFactory dbConnectionFactory) : IAppEventRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task<bool> CreateAsync(AppEvent appEvent, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            INSERT INTO events (id, group_id, title, slug, description, location, start_time, end_time, created_by_user_id, created_on)
            VALUES (@Id, @GroupId, @Title, @Slug, @Description, @Location, @StartTime, @EndTime, @CreatedByUserId, @CreatedOn)
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, appEvent, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw ex.ConstraintName switch
            {
                "events_slug_active_idx" => new DuplicateException(Errors.AppEvents.DuplicateSlugErrorCode, Messages.AppEvents.DuplicateSlug),
                _ => new DuplicateException(Errors.AppEvents.DuplicateErrorCode, Messages.AppEvents.Duplicate)
            };
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.ForeignKeyViolation)
        {
            throw ex.ConstraintName switch
            {
                "fk_event_group" => new ForeignKeyViolationException(Errors.AppEvents.InvalidGroupErrorCode, Messages.AppEvents.InvalidGroup),
                "fk_event_creator" => new ForeignKeyViolationException(Errors.AppEvents.InvalidUserErrorCode, Messages.AppEvents.InvalidUser),
                _ => new ForeignKeyViolationException(Errors.AppEvents.InvalidReferenceErrorCode, Messages.AppEvents.InvalidReference)
            };
        }
    }
    public async Task<AppEvent?> GetByIdAsync(Guid id, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
            SELECT id, group_id AS GroupId, title, slug, description, location, start_time AS StartTime, end_time AS EndTime, created_by_user_id AS CreatedByUserId, created_on AS CreatedOn 
            FROM events
            WHERE id = @Id 
            AND deleted_on IS NULL;

            SELECT t.id, t.name, t.color_hex AS ColorHex, t.created_on AS CreatedOn
            FROM event_tags et
            INNER JOIN tags t ON et.tag_id = t.id
            WHERE et.event_id = @Id 
            AND t.deleted_on IS NULL;

            SELECT user_id AS UserId, event_id AS EventId, status, responded_on AS RespondedOn
            FROM rsvps
            WHERE event_id = @Id 
            AND deleted_on IS NULL;
            """;

        using var multi = await connection.QueryMultipleAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: token));

        var appEvent = await multi.ReadFirstOrDefaultAsync<AppEvent>();

        if (appEvent is null)
            return null;

        appEvent.Tags = [.. await multi.ReadAsync<Tag>()];
        appEvent.Rsvps = [.. await multi.ReadAsync<Rsvp>()];

        return appEvent;
    }

    public async Task<AppEvent?> GetBySlugAsync(string slug, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT id, group_id AS GroupId, title, slug, description, location, start_time AS StartTime, end_time AS EndTime, created_by_user_id AS CreatedByUserId, created_on AS CreatedOn 
                FROM events
                WHERE slug = @Slug
                AND deleted_on IS NULL;
                """;

        var appEvent = await connection.QueryFirstOrDefaultAsync<AppEvent>(new CommandDefinition(sql, new { Slug = slug }, cancellationToken: token));

        if (appEvent is null)
            return null;

        var relatedSql = """
                SELECT t.id, t.name, t.color_hex AS ColorHex, t.created_on AS CreatedOn
                FROM event_tags et
                INNER JOIN tags t ON et.tag_id = t.id
                WHERE et.event_id = @EventId 
                AND t.deleted_on IS NULL;

                SELECT user_id AS UserId, event_id AS EventId, status, responded_on AS RespondedOn
                FROM rsvps
                WHERE event_id = @EventId 
                AND deleted_on IS NULL;
                """;
                
        using var multi = await connection.QueryMultipleAsync(new CommandDefinition(relatedSql, new { EventId = appEvent.Id }, cancellationToken: token));

        appEvent.Tags = [.. await multi.ReadAsync<Tag>()];
        appEvent.Rsvps = [.. await multi.ReadAsync<Rsvp>()];

        return appEvent;
    }

    public async Task<IEnumerable<AppEvent>> GetAllAsync(CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = """
                SELECT e.id, e.group_id AS GroupId, e.title, e.slug, e.description, e.location, e.start_time AS StartTime, e.end_time AS EndTime, e.created_by_user_id AS CreatedByUserId, e.created_on AS CreatedOn,
                t.id AS Id, t.name, t.color_hex AS ColorHex, t.created_on AS CreatedOn,
                r.user_id AS UserId, r.event_id AS EventId, r.status, r.responded_on AS RespondedOn
                FROM events e
                LEFT JOIN event_tags et ON e.id = et.event_id
                LEFT JOIN tags t ON et.tag_id = t.id AND t.deleted_on IS NULL
                LEFT JOIN rsvps r ON e.id = r.event_id AND r.deleted_on IS NULL
                WHERE e.deleted_on IS NULL
                ORDER BY e.id;
                """;

        var eventDictionary = new Dictionary<Guid, AppEvent>();

        var events = await connection.QueryAsync<AppEvent, Tag, Rsvp, AppEvent>(
            new CommandDefinition(sql, cancellationToken: token),
                (appEvent, tag, rsvp) =>
                    {
                        if (!eventDictionary.TryGetValue(appEvent.Id, out var currentEvent))
                        {
                            currentEvent = appEvent;
                            currentEvent.Tags = new List<Tag>();
                            currentEvent.Rsvps = new List<Rsvp>();
                            eventDictionary.Add(currentEvent.Id, currentEvent);
                        }

                        if (tag is not null && !currentEvent.Tags.Any(t => t.Id == tag.Id))
                            currentEvent.Tags.Add(tag);

                        if (rsvp is not null && !currentEvent.Rsvps.Any(r => r.UserId == rsvp.UserId))
                            currentEvent.Rsvps.Add(rsvp);

                        return currentEvent;
                    },
                splitOn: "Id,UserId"
            );

        return eventDictionary.Values;
    }


    public async Task<bool> UpdateAsync(AppEvent appEvent, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE events SET group_id = @GroupId, title = @Title, slug = @Slug, description = @Description, location = @Location, 
            start_time = @StartTime, end_time = @EndTime, updated_on = @UpdatedOn
            WHERE id = @Id;
            """;

        try
        {
            var result = await connection.ExecuteAsync(new CommandDefinition(sql, appEvent, transaction, cancellationToken: token));

            transaction.Commit();
            return result > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.UniqueViolation)
        {
            throw ex.ConstraintName switch
            {
                "events_slug_active_idx" => new DuplicateException(Errors.AppEvents.DuplicateSlugErrorCode, Messages.AppEvents.DuplicateSlug),
                _ => new DuplicateException(Errors.AppEvents.DuplicateErrorCode, Messages.AppEvents.Duplicate)
            };
        }
        catch (PostgresException ex) when (ex.SqlState == Npgsql.PostgresErrorCodes.ForeignKeyViolation)
        {
            throw ex.ConstraintName switch
            {
                "fk_event_group" => new ForeignKeyViolationException(Errors.AppEvents.InvalidGroupErrorCode, Messages.AppEvents.InvalidGroup),
                "fk_event_creator" => new ForeignKeyViolationException(Errors.AppEvents.InvalidUserErrorCode, Messages.AppEvents.InvalidUser),
                _ => new ForeignKeyViolationException(Errors.AppEvents.InvalidReferenceErrorCode, Messages.AppEvents.InvalidReference)
            };
        }
    }

    public async Task<bool> SoftDeleteAsync(Guid id, DateTimeOffset deletedOn, CancellationToken token)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var sql = """
            UPDATE events SET updated_on = @UpdatedOn, deleted_on = @DeletedOn
            WHERE id = @Id
            AND deleted_on IS NULL;
            """;

        var result = await connection.ExecuteAsync(new CommandDefinition(sql, new {Id = id, DeletedOn = deletedOn, UpdatedOn = deletedOn}, transaction, cancellationToken: token));

        transaction.Commit();
        return result > 0;
    }

    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}