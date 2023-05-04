using Microsoft.AspNetCore.Mvc;


namespace WebApplication22.Controllers
{
    [Route("simpra/v1.0/api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("GetQuery")]
        public string GetQuery([FromQuery] int id = 0, int lat=0, int lng=0)
        {
            return $"{id}-{lat}-{lng}";
        }

        [HttpGet("GetRoute/{id}")]
        public string GetRoute([FromRoute] int id, int lat, int lng)
        {
            return $"{id}-{lat}-{lng}";
        }

        [HttpGet("GetRoute2/{id}/{lat}/{lng}")]
        public string GetRoute2(int id, int lat, int lng)
        {
            return $"{id}-{lat}-{lng}";
        }

        [HttpGet("GetQuery2")]
        public string GetQuery2([FromQuery] string? id, string? lat, string? lng)
        {
            return $"{id}-{lat}-{lng}";
        }

    }
}
