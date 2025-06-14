using System.Data;

namespace RendezVoulns.Application.Persistence.Database;

public interface IDbConnectionFactory
{
    Task <IDbConnection> CreateConnectionAsync();
}