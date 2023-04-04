using DapperUnitOfWork.Data.Context.Interfaces;
using DapperUnitOfWork.Data.Models.Request;
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
    public async Task<string> Test()
    {
        try
        {
            var address1 = await _personDbContext.Addresses.GetFirstOrDefaultByIdAsync(1);

            await _personDbContext.BeginTransactionAsync();

            var result1 =
                await _personDbContext.Addresses.UpdateAddressByIdAsync(new UpdateAddressByIdRequest(1, "11111"));
            var result2 =
                await _personDbContext.Addresses.UpdateAddressByIdAsync(new UpdateAddressByIdRequest(2, "22222"));

            await _personDbContext.CommitAsync();

            var result3 =
                await _personDbContext.Addresses.UpdateAddressByIdAsync(new UpdateAddressByIdRequest(3, "33333"));

            await _personDbContext.BeginTransactionAsync();

            var result4 =
                await _personDbContext.Addresses.UpdateAddressByIdAsync(new UpdateAddressByIdRequest(4, "44444"));
            var result5 =
                await _personDbContext.Addresses.UpdateAddressByIdAsync(new UpdateAddressByIdRequest(5, "55555"));

            await _personDbContext.CommitAsync();

            return "Ok";
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            await _personDbContext.RollbackAsync();

            return "Bad";
        }
    }
}