using DapperUnitOfWork.Common.Data.Models;
using DapperUnitOfWork.Data.Models.Request;

namespace DapperUnitOfWork.Data.Repositories.Interfaces;

public interface IAddressRepository : IRepository<Address>
{
    Task<int> UpdateAddressByIdAsync(UpdateAddressByIdRequest request);
}