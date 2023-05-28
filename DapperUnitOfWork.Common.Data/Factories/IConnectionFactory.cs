using System.Data.Common;

namespace DapperUnitOfWork.Common.Data.Factories;

public interface IConnectionFactory
{
    DbConnection? OpenConnection();
    Task<DbConnection> OpenConnectionAsync();
}