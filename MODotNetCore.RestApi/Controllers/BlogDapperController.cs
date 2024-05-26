using Dapper;
using Microsoft.AspNetCore.Mvc;
using MODotNetCore.RestApi.Model;
using MODotNetCore.RestApi.Queries;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MODotNetCore.RestApi.Controllers;

[Route("api/BlogDapper")]
[ApiController]
public class BlogDapperController : ControllerBase
{
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
        string message = string.Empty;
        try
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);
            BlogModel newBlog = new BlogModel();
            string query = CommonQuery.CreateQuery;
            var result = db.Execute(query, newBlog);
            message = result > 0 ? "\n\n Saving Successful" : "Saving Failed";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel blogs)
    {
        string message = string.Empty;

        try
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);

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
        return Ok(message);
    }


    [HttpPatch("{id}")]
    public IActionResult UpdatePatch(int id, BlogModel blogs)
    {
        string message = string.Empty;
        string conditions = string.Empty;

        try
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);

            var item = db.Query<BlogModel>(CommonQuery.GetDataById, new BlogModel { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                message = "Data Not Found!";
                goto Results;
            }
            if (!string.IsNullOrEmpty(blogs.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blogs.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blogs.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }
            blogs.BlogId = id;
            var res = db.Execute(CommonQuery.UpdatePatch, blogs);
            message = res > 0 ? "Update Successful" : "Update Failed";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
        Results:
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        string message = string.Empty;
        var res = 0;
        try
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.connection.ConnectionString);
            var item = db.Query<BlogModel>(CommonQuery.GetDataById, new BlogModel { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                message = "Data Not Found!";
                goto Results;
            }

            string query = CommonQuery.DeleteQuery;
            res = db.Execute(query, item);
            message = res > 0 ? "\n\n Delete Successful" : "Delete Failed";

            Results:
            return Ok(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}
