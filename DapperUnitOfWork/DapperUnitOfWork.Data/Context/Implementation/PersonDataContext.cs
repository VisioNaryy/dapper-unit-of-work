using System.Data;
using DapperUnitOfWork.Common.Data.Factories.Interfaces;
using DapperUnitOfWork.Data.Context.Interfaces;
using DapperUnitOfWork.Data.Repositories.Implementation;
using DapperUnitOfWork.Data.Repositories.Interfaces;

namespace DapperUnitOfWork.Data.Context.Implementation;

public sealed class PersonDataContext : IPersonDataContext
{
    private bool _disposed;
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    private IPersonRepository? _personRepository;

    public PersonDataContext(IConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.OpenConnection();
    }
    
    public IDbConnection? Connection => _connection;
    public IDbTransaction? Transaction => _transaction;
    
    public IPersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(this);

    public void Begin()
    {
        _transaction = _connection?.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        _transaction = null;
        _connection = null;

        _disposed = true;
    }
}