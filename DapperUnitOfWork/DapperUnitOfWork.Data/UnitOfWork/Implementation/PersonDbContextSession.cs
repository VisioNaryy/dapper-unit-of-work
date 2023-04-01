using System.Data;
using DapperUnitOfWork.Common.Data.Factories.Interfaces;
using DapperUnitOfWork.Data.UnitOfWork.Interfaces;

namespace DapperUnitOfWork.Data.UnitOfWork.Implementation;

public class PersonDbContextSession : IPersonDbContextSession
{
    private bool _disposed;
    private IDbTransaction? _transaction;
    private IDbConnection? _connection;

    public PersonDbContextSession(ISqlConnectionFactory sqlConnectionFactory)
    {
        _connection = sqlConnectionFactory.OpenConnection();
    }

    public IDbConnection? Connection => _connection;
    public IDbTransaction? Transaction => _transaction;

    public void Begin()
    {
        _transaction = _connection?.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
            _transaction?.Connection?.Close();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction?.Connection?.Dispose();
            _transaction = null;
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction?.Rollback();
            _transaction?.Connection?.Close();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction?.Connection?.Dispose();
            _transaction = null;
        }
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
            _transaction?.Connection?.Close();
            _transaction?.Connection?.Dispose();
            
            _connection?.Close();
            _connection?.Dispose();
        }
    
        _transaction = null;
        _connection = null;
    
        _disposed = true;
    }
}