using System.Data;
using Dapper;
using DapperUnitOfWork.Common.Data.Models;
using DapperUnitOfWork.Data.Context.Interfaces;
using DapperUnitOfWork.Data.Models.Request;
using DapperUnitOfWork.Data.Repositories.Interfaces;

namespace DapperUnitOfWork.Data.Repositories.Implementation;

public class PersonRepository : IPersonRepository
{
    private readonly IDbTransaction? _transaction;
    private readonly IDbConnection? _connection;

    public PersonRepository(
        IPersonDataContext personDataContext)
    {
        _connection = personDataContext.Connection;
        _transaction = personDataContext.Transaction;
    }

    public async Task<Address?> GetAddressByIdAsync(int addressId)
    {
        var sql = """
        SELECT TOP 1 *
        FROM Person.Address
        WHERE AddressID = @addressId
        """;

        var result = await _connection.QueryFirstOrDefaultAsync<Address>(
            sql,
            new
            {
                addressId
            },
            transaction: _transaction);

        return result;
    }

    public async Task<int> UpdateAddressByIdAsync(UpdateAddressByIdRequest request)
    {
        var (addressId, postalCode) = request;

        var sql = """
        UPDATE Person.Address
        SET PostalCode = @postalCode
        WHERE AddressID = @addressId
        """;
        
        var result = await _connection.ExecuteScalarAsync<int>(
            sql,
            new
            {
                postalCode,
                addressId
            },
            transaction: _transaction);

        return result;
    }
}