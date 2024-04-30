using Dapper;
using Microsoft.AspNetCore.Mvc;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.RestApi.Model;
using MODotNetCore.RestApi.Services;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [HttpPost]
        public IActionResult Create()
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);
                BlogModel newBlog = new BlogModel();
                string query = CommonQuery.CreateQuery;
                var result = db.Execute(query, newBlog);
                string message = result > 0 ? "\n\n Saving Successful" : "Saving Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogs)
        {
            try
            {
                using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);

                string message = string.Empty;
                var item = db.Query<BlogModel>(CommonQuery.GetDataById, new BlogModel { BlogId = id }).FirstOrDefault();
                if (item is null)
                {
                    message = "Data Not Found!";
                    goto Results;
                }
                blogs.BlogId = id;
                var res = db.Execute(CommonQuery.UpdateQuery, blogs);
                message = res > 0 ? "Update Successful" : "Update Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            Results:
            return Ok();
        }
    }
}
