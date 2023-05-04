using Microsoft.AspNetCore.Mvc;
using SimpApi.Base;

namespace SimpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : BaseApiController
{
    public TestController()
    {

    }


    [HttpGet]
    public string Get(string name)
    {
        return name;
    }
}
