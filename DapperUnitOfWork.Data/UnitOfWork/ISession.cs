using System.Data.Common;
using DapperUnitOfWork.Common.Data.Factories;

namespace DapperUnitOfWork.Data.UnitOfWork;

public interface ISession : IAsyncDisposable
{
    DbConnection? Connection { get; set; }
    DbTransaction? Transaction { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task OpenConnectionAsync();
    Task CloseConnectionAsync();
}