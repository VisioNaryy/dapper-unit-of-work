using DapperUnitOfWork.Data.Repositories.Interfaces;

namespace DapperUnitOfWork.Data.Context.Interfaces;

public interface IPersonDbContext : IDbContext
{
    IAddressRepository Addresses { get; }
}