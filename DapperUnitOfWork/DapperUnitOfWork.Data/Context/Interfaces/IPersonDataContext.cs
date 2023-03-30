using System.Data;
using DapperUnitOfWork.Data.Repositories.Interfaces;

namespace DapperUnitOfWork.Data.Context.Interfaces;

public interface IPersonDataContext : IDisposable
{
    IPersonRepository PersonRepository { get; }
    IDbConnection? Connection { get; }
    IDbTransaction? Transaction { get; }
    void Begin();
    void Commit();
    void Rollback();
}