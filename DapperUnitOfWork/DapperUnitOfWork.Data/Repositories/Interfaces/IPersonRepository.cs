using DapperUnitOfWork.Common.Data.Models;
using DapperUnitOfWork.Data.Models.Request;

namespace DapperUnitOfWork.Data.Repositories.Interfaces;

public interface IPersonRepository
{
    Task<Address?> GetAddressByIdAsync(int addressId);
    Task<int> UpdateAddressByIdAsync(UpdateAddressByIdRequest request);
}