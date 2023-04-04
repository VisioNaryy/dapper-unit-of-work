using Dapper;
using DapperUnitOfWork.Common.Data.Models;
using DapperUnitOfWork.Data.Models.Request;
using DapperUnitOfWork.Data.Repositories.Interfaces;
using DapperUnitOfWork.Data.UnitOfWork.Interfaces;

namespace DapperUnitOfWork.Data.Repositories.Implementation;

public class AddressRepository : IAddressRepository
{
    private readonly IPersonDbContextSession _session;

    public AddressRepository(
        IPersonDbContextSession session)
    {
        _session = session;
    }

    public async Task<Address?> GetFirstOrDefaultByIdAsync(int id)
    {
        var sql = """
        SELECT TOP 1 *
        FROM Person.Address
        WHERE AddressID = @id
        """;

        var result = await _session.Connection.QueryFirstOrDefaultAsync<Address>(
            sql,
            new
            {
                id
            },
            _session.Transaction);

        return result;
    }

    public async Task<int> UpdateAddressByIdAsync(UpdateAddressByIdRequest request)
    {
        var (addressId, postalCode) = request;

        // if (addressId is 2)
        //     throw new Exception();

        var sql = """
        UPDATE Person.Address
        SET PostalCode = @postalCode
        WHERE AddressID = @addressId
        """;

        var result = await _session.Connection.ExecuteScalarAsync<int>(
            sql,
            new
            {
                postalCode,
                addressId
            },
            _session.Transaction);

        return result;
    }
}