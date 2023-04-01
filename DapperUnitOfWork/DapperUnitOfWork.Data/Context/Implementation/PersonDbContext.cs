using DapperUnitOfWork.Data.Context.Interfaces;
using DapperUnitOfWork.Data.Repositories.Implementation;
using DapperUnitOfWork.Data.Repositories.Interfaces;
using DapperUnitOfWork.Data.UnitOfWork.Interfaces;

namespace DapperUnitOfWork.Data.Context.Implementation;

public sealed class PersonDbContext : IPersonDbContext
{
    private readonly IPersonDbContextSession _session;
    private IAddressRepository? _addressRepository;

    public PersonDbContext(IPersonDbContextSession session)
    {
        _session = session;
    }
    
    public IAddressRepository Addresses =>
        _addressRepository ??= new AddressRepository(_session);

    public void Begin()
    {
        _session.Begin();
    }

    public void Commit()
    {
        _session.Commit();
    }

    public void Rollback()
    {
        _session.Rollback();
    }
}