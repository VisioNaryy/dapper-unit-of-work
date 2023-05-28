using DapperUnitOfWork.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperUnitOfWork.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DapperRepositoryController : ControllerBase
{
    private readonly IAddressRepository _addressRepository;

    public DapperRepositoryController(
        IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    [HttpGet]
    public async Task Test()
    {
        var address1 = await _addressRepository.GetFirstOrDefaultByIdAsync(1);
    }
}