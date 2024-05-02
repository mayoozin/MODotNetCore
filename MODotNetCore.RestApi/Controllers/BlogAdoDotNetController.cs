using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.RestApi.Model;
using MODotNetCore.RestApi.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MODotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<BlogDataModel> list = new List<BlogDataModel>
            try
            {
                using SqlConnection _connectionString = new SqlConnection(ConnectionStrings.connection.ConnectionString);
                _connectionString.Open();

                string query = CommonQuery.SelectQuery;
                SqlCommand cmd = new SqlCommand(query, _connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                list = dt.AsEnumerable()
                    .Select(dr => new BlogDataModel()
                    {
                        BlogId = Convert.ToInt32(dr["BlogId"]),
                        BlogTitle = Convert.ToString(dr["BlogTitle"]),
                        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                        BlogContent = Convert.ToString(dr["BlogContent"])
                    }).ToList();

                _connectionString.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetByBlogId(int blogId)
        {
            var item = new BlogDataModel();
            try
            {
                using SqlConnection _connectionString = new SqlConnection(ConnectionStrings.connection.ConnectionString);
                _connectionString.Open();

                string query = CommonQuery.UpdateQuery;
                SqlCommand cmd = new SqlCommand(query, _connectionString);
                cmd.Parameters.AddWithValue("@BlogId", blogId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                _connectionString.Close();

                if (dt.Rows.Count == 0)
                {
                    return NotFound("No Data");
                }

                DataRow dr = dt.Rows[0];
                item = new BlogDataModel()
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    BlogTitle = Convert.ToString(dr["BlogTitle"]),
                    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                    BlogContent = Convert.ToString(dr["BlogContent"])
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogDataModel newBlog)
        {
            string message = string.Empty;
            int result = 0;
            try
            {
                using SqlConnection _connectionString = new SqlConnection(ConnectionStrings.connection.ConnectionString);
                _connectionString.Open();

                string query = CommonQuery.CreateQuery;
                SqlCommand cmd = new SqlCommand(query, _connectionString);
                cmd.Parameters.AddWithValue("@BlogTitle", newBlog.BlogTitle);
                cmd.Parameters.AddWithValue("@BlogAuthor", newBlog.BlogAuthor);
                cmd.Parameters.AddWithValue("@BlogContent", newBlog.BlogContent);
                result = cmd.ExecuteNonQuery();
                _connectionString.Close();

                message = result > 0 ? "Saving Successful" : "Saving Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return Ok(message);
        }
    }
}
