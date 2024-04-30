using Dapper;
using Microsoft.AspNetCore.Mvc;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.RestApi.Model;
using MODotNetCore.RestApi.Services;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

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
            using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);
            List<BlogModel> blogs = db.Query<BlogModel>(CommonQuery.SelectQuery)
               .ToList();
            return Ok(blogs);
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
