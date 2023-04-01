using System.Data;

namespace DapperUnitOfWork.Common.Data.Factories;

public interface IConnectionFactory
{
    IDbConnection? OpenConnection();
}