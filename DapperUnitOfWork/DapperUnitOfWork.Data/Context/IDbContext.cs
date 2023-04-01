namespace DapperUnitOfWork.Data.Context;

public interface IDbContext
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}