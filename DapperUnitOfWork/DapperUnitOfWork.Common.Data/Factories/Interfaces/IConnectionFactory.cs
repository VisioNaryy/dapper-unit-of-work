using System.Data;

namespace DapperUnitOfWork.Common.Data.Factories.Interfaces;

public interface IConnectionFactory
{
    IDbConnection? OpenConnection();
}