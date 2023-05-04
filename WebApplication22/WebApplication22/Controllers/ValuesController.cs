using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication22.Controllers
{
    [Route("simpra/v1.0/api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get([FromRoute] int id)
        {
            return "value" + id;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return value;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return id  + "-" + value;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public string Delete([FromRoute]  int id)
        {
            return "delete " + id;
        }




    }
}
