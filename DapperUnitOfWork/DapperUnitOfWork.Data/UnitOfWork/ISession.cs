using System.Data.Common;

namespace DapperUnitOfWork.Data.UnitOfWork;

public interface ISession : IAsyncDisposable
{
    DbConnection? Connection { get; }
    DbTransaction? Transaction { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}