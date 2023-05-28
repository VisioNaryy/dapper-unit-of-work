using System.Data;
using System.Data.Common;
using DapperUnitOfWork.Common.Data.Factories;

namespace DapperUnitOfWork.Data.UnitOfWork.Implementation;

public abstract class Session : ISession
{
    private readonly IConnectionFactory _connectionFactory;
    private bool _disposed;

    protected Session(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public DbConnection? Connection { get; set; }

    public DbTransaction? Transaction { get; private set; }

    public async Task OpenConnectionAsync()
    {
        if (Connection is not null)
            return;

        Connection = await _connectionFactory.OpenConnectionAsync();
    }
    
    public async Task CloseConnectionAsync()
    {
        if (Connection is null)
            return;

        if (Connection.State is ConnectionState.Open)
            await Connection.CloseAsync();

        await Connection.DisposeAsync();

        Connection = null;
    }
    
    public async Task BeginTransactionAsync()
    {
        if (Connection is null || Transaction is not null)
            return;

        Transaction = await Connection.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (Transaction is null)
            return;

        try
        {
            await Transaction.CommitAsync();
            throw new Exception();
        }
        catch
        {
            await Transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await Transaction.DisposeAsync();

            Transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (Transaction is null)
            return;

        await Transaction.RollbackAsync();

        await Transaction.DisposeAsync();

        Transaction = null;
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
            if (Transaction is not null)
                await Transaction.DisposeAsync();


            if (Connection is not null)
            {
                if (Connection.State is ConnectionState.Open)
                    await Connection.CloseAsync();

                await Connection.DisposeAsync();
            }
        }

        Transaction = null;
        Connection = null;

        _disposed = true;
    }
}