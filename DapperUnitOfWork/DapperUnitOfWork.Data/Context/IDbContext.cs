namespace DapperUnitOfWork.Data.Context;

public interface IDbContext
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}