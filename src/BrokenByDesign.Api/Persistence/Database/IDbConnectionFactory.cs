using System.Data;

namespace BrokenByDesign.Api.Persistence.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}