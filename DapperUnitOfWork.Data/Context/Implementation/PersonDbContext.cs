﻿using DapperUnitOfWork.Data.Context.Interfaces;
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

    public async Task BeginTransactionAsync()
    {
        await _session.OpenConnectionAsync();
        await _session.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _session.CommitAsync();
        await _session.CloseConnectionAsync();
    }

    public async Task RollbackAsync()
    {
        await _session.RollbackAsync();
        await _session.CloseConnectionAsync();
    }
}