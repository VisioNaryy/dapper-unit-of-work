using System.Data.Common;

namespace DapperUnitOfWork.Data.UnitOfWork;

public interface ISession : IAsyncDisposable
{
    DbConnection? Connection { get; }
    DbTransaction? Transaction { get; }
    void BeginTransaction();
    void Commit();
    void Rollback();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}