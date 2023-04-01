namespace DapperUnitOfWork.Data.Context;

public interface IDbContext
{
    void Begin();
    void Commit();
    void Rollback();
}