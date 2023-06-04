using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Service.Filters;

namespace SimApi.Service.Controllers;


[TypeFilter(typeof(LogExceptionFilter))]
[Route("simapi/v1/[controller]")]
[ApiController]
[EnableMiddlewareLogger]
[ResponseGuid]
[NonController]
public class AttibuteTestController : ControllerBase
{

    public AttibuteTestController()
    {

    }

    [HttpGet]
    [ResponseHeader("ActionName", "/AttibuteTest/Get")]
    public string Get()
    {
        return "Hello 1";
    }


    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogActionFilter))]
    [TypeFilter(typeof(LogAuthorizationFilter))]
    [TypeFilter(typeof(LogResultFilter))]
    [TypeFilter(typeof(LogExceptionFilter))]
    [HttpGet("Filters")]
 
    public string Filters()
    {
        return "Hello 2";
    }



    [HttpGet("Exception")]
    public async Task<IActionResult> Exception()
    {
        throw new Exception("Thow Exception");
    }

}
