using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class CurrencyJobController : ControllerBase
{
    private readonly ICurrencyService service;

    public CurrencyJobController(ICurrencyService service)
    {
        this.service = service;
    }

    [HttpPut("Update")]
    public ApiResponse GetAll()
    {
        BackgroundJob.Schedule(() => service.GetAll(), TimeSpan.FromMinutes(2));
        return new ApiResponse();
    }


}
