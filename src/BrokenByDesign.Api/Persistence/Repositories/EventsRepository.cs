
using System.Data;
using BrokenByDesign.Api.Domain.Entities;
using BrokenByDesign.Api.Persistence.Database;
using Dapper;
using Throw;

namespace BrokenByDesign.Api.Persistence.Repositories;

public class EventsRepository(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task CreateAsync(Event @event) 
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
            INSERT INTO events(id, title, description, location, start_time, end_time, created_by_user_id, created_on)
            VALUES (@Id, @Title, @Description, @Location, @StartTime, @EndTime, @CreatedByUserId, @CreatedOn)";

        var result = await connection.ExecuteAsync(query, @event);

        result.Throw().IfNegativeOrZero();
    }

    public async Task<Event?> GetByIdAsync(Guid eventId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
            SELECT * 
            FROM events 
            WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Event>(query, new { Id = eventId });
    }
}