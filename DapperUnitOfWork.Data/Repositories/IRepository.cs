namespace DapperUnitOfWork.Data.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetFirstOrDefaultByIdAsync(int id);
}