using DapperUnitOfWork.Data.Context.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperUnitOfWork.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DapperController : ControllerBase
{
    private readonly IPersonDbContext _personDbContext;

    public DapperController(
        IPersonDbContext personDbContext)
    {
        _personDbContext = personDbContext;
    }

    [HttpPost]
    public async Task Test()
    {
        try
        {
            var address1 = await _personDbContext.Addresses.GetFirstOrDefaultByIdAsync(1);
            
            _personDbContext.Begin();

            var result1 = await _personDbContext.Addresses.UpdateAddressByIdAsync(new(1, "11111"));
            var result2 = await _personDbContext.Addresses.UpdateAddressByIdAsync(new(2, "22222"));

            _personDbContext.Commit();
            
            var result3 = await _personDbContext.Addresses.UpdateAddressByIdAsync(new(3, "33333"));
            
            _personDbContext.Begin();
            
            var result4 = await _personDbContext.Addresses.UpdateAddressByIdAsync(new(4, "44444"));
            var result5 = await _personDbContext.Addresses.UpdateAddressByIdAsync(new(5, "55555"));
            
            _personDbContext.Commit();
            
            Console.WriteLine();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            _personDbContext.Rollback();
        }
    }
}