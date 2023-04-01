﻿using System.Data.Common;
using DapperUnitOfWork.Common.Data.Factories;

namespace DapperUnitOfWork.Data.UnitOfWork.Implementation;

public abstract class Session : ISession
{
    private bool _disposed;
    private DbTransaction? _transaction;
    private DbConnection? _connection;

    public DbConnection? Connection => _connection;
    public DbTransaction? Transaction => _transaction;
    
    public Session(IConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.OpenConnection();
    }

    public void BeginTransaction()
    {
        _transaction = _connection?.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction?.Rollback();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task BeginTransactionAsync()
    {
        if (_connection is not null)
            _transaction = await _connection.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction is null)
            return;
        
        try
        {
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();

            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction is null)
            return;
        
        try
        {
            await _transaction.RollbackAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();

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
            
            _connection?.Close();
            _connection?.Dispose();
        }

        _transaction = null;
        _connection = null;

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true).ConfigureAwait(false);
        
        GC.SuppressFinalize(this);
    }

    private async Task DisposeAsync(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
            }

            if (_connection is not null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }
        }

        _transaction = null;
        _connection = null;

        _disposed = true;
    }
}