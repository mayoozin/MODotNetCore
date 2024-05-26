using Dapper;
using MODotNetCore.ConsoleApp.Commons;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.DapperExample;

public class DapperServices
{
    private readonly DbConnectionServices _dbConnection;

    public DapperServices(DbConnectionServices dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void Read()
    {
        using IDbConnection db = new SqlConnection(_dbConnection.GetConnectionString());
        List<BlogModel> blogs = db.Query(CommonQuery.SelectQuery)
            .Select(x => new BlogModel()
            {
                BlogId = x.BlogId.ToString(),
                BlogTitle = x.BlogTitle,
                BlogAuthor = x.BlogAuthor,
                BlogContent = x.BlogContent
            })
            .ToList();
        foreach (BlogModel item in blogs)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("----------------------------");
        }
        Console.ReadLine();
        Console.Clear();
    }

    public void Create()
    {
        try
        {
            using IDbConnection db = new SqlConnection(_dbConnection.GetConnectionString());

            Console.WriteLine("Connection Open \n\n");
            BlogModel newBlog = Common.GetBlogData();

            string query = CommonQuery.CreateQuery;
            var result = db.Execute(query, newBlog);
            string message = result > 0 ? "\n\n Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public void Update()
    {
        string message = string.Empty;
        var res = 0;
        int blogId = 0;
        try
        {
            using IDbConnection db = new SqlConnection(_dbConnection.GetConnectionString());

            Console.WriteLine("\n\nPlease type the Id of the record would like to Update. Type 0 to return to main menu.\n\n");
            string commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                message = "\nYou have to type an Id.\n";
                goto Result;
            }
            Common common = new Common();
            blogId = int.Parse(commandInput);

            if (blogId == 0) common.GetUserCommand();
            Console.WriteLine("Connection Open \n\n");
            BlogModel newBlog = Common.GetBlogData();

            string query = CommonQuery.UpdateQuery;
            newBlog.BlogId = blogId.ToString();
            res = db.Execute(query, newBlog);
            message = res > 0 ? "\n\n Update Successful" : "Update Failed";

            Result:
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public void Delete()
    {
        int blogId = 0;
        string message = string.Empty;
        var res = 0;
        try
        {
            using IDbConnection db = new SqlConnection(_dbConnection.GetConnectionString());

            Console.WriteLine("\n\nPlease type the Id of the record would like to Delete. Type 0 to return to main menu.\n\n");
            string commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                message = "\nYou have to type an Id.\n";
                goto Result;
            }
            Common common = new Common();
            blogId = int.Parse(commandInput);
            var item = new BlogModel
            {
                BlogId = blogId.ToString()
            };

            if (blogId == 0) common.GetUserCommand();

            Console.WriteLine("Connection Open \n\n");

            string query = CommonQuery.DeleteQuery;
            res = db.Execute(query, item);
            message = res > 0 ? "\n\n Delete Successful" : "Delete Failed";

            Result:
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public void SelectDataById()
    {

    }

}
