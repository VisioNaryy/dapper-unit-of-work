using System.Data;
using System.Data.SqlClient;
using DapperUnitOfWork.Common.Data.Factories.Interfaces;
using DapperUnitOfWork.Common.Data.IOptions;
using Microsoft.Extensions.Options;

namespace DapperUnitOfWork.Common.Data.Factories.Implementation;

public class SqlConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;
    
    public SqlConnectionFactory(IOptionsMonitor<SqlServerOptions> options)
    {
        _connectionString = options.CurrentValue.DefaultConnection;
    }

    public IDbConnection? OpenConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}