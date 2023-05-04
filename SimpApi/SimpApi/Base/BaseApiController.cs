using Microsoft.AspNetCore.Mvc;

namespace SimpApi.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    [HttpGet("HeartBeat")]
    public string HeartBeat()
    {
        return DateTime.UtcNow.ToShortDateString();
    }
}
