using System.Data;

namespace DapperUnitOfWork.Data.UnitOfWork;

public interface ISession : IDisposable
{
    IDbConnection? Connection { get; }
    IDbTransaction? Transaction { get; }
    void Begin();
    void Commit();
    void Rollback();
}