using DapperUnitOfWork.Common.Data.Factories.Interfaces;
using DapperUnitOfWork.Data.UnitOfWork.Interfaces;

namespace DapperUnitOfWork.Data.UnitOfWork.Implementation;

public class PersonDbContextSession : Session, IPersonDbContextSession
{
    public PersonDbContextSession(ISqlConnectionFactory sqlConnectionFactory) : base(sqlConnectionFactory)
    {
    }
}