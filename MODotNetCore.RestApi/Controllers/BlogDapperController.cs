using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MODotNetCore.RestApi.Controllers
{
    [Route("api/BlogDapper")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        // GET: api/<BlogDapperController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<BlogDapperController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlogDapperController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BlogDapperController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogDapperController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
