using DapperUnitOfWork.Data.Context.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperUnitOfWork.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DapperController : ControllerBase
{
    private readonly IPersonDataContext _personDataContext;

    public DapperController(
        IPersonDataContext personDataContext)
    {
        _personDataContext = personDataContext;
    }

    [HttpPost]
    public async Task Test()
    {
        try
        {
            _personDataContext.Begin();
        
            var personRepositoy = _personDataContext.PersonRepository;

            var result1 = await personRepositoy.UpdateAddressByIdAsync(new(1, "11111"));
            var result2 = await personRepositoy.UpdateAddressByIdAsync(new (2, "22222"));

            _personDataContext.Commit();

            Console.WriteLine();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            _personDataContext.Rollback();
        }
    }
}