using System.Data.SqlClient;
using System.Data;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Commons;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.ConsoleApp.Services;
using Microsoft.Extensions.Configuration;

namespace MODotNetCore.ConsoleApp.EFCoreExamples;

public class AdoDotNetServices
{
    DbConnectionServices connectionServices = new DbConnectionServices();
    string? _connectionString;
    public void Read()
    {
        _connectionString = connectionServices.GetConnectionString();
        try
        {
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                Console.WriteLine("____Connection Open_____\n\n");

                string query = CommonQuery.SelectQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("Blog Id => " + dr["BlogId"]);
                    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
                    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
                }
                Console.ReadLine();
                Console.Clear();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public void Create()
    {
        _connectionString = connectionServices.GetConnectionString();
        int result = 0;
        try
        {
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                Console.WriteLine("Connection Open \n\n");
                BlogModel newBlog = GetBlogData();

                string query = CommonQuery.CreateQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BlogTitle", newBlog.BlogTitle);
                cmd.Parameters.AddWithValue("@BlogAuthor", newBlog.BlogAuthor);
                cmd.Parameters.AddWithValue("@BlogContent", newBlog.BlogContent);
                result = cmd.ExecuteNonQuery();

                string message = result > 0 ? "Saving Successful" : "Saving Failed";
                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public void Update()
    {
        _connectionString = connectionServices.GetConnectionString();
        int blogId = 0;
        string message = string.Empty;
        int result = 0;
        try
        {
            Console.WriteLine("\n\nPlease type the Id of the record would like to Update. Type 0 to return to main menu.\n\n");
            string commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nYou have to type an Id.\n");
                Delete();
            }
            Common common = new Common();
            blogId = int.Parse(commandInput);

            if (blogId == 0) common.GetUserCommand();

            using (SqlConnection con = new(_connectionString))
            {

                #region 💕 Check Data 💕

                bool data = GetDataById(blogId);
                if (!data)
                {
                    message = "There is no Data!";
                    goto Result;
                }

                #endregion

                con.Open();
                Console.WriteLine("Connection Open \n\n");

                BlogModel newBlog = GetBlogData();

                string query = CommonQuery.UpdateQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BlogId", blogId);
                cmd.Parameters.AddWithValue("@BlogTitle", newBlog.BlogTitle);
                cmd.Parameters.AddWithValue("@BlogAuthor", newBlog.BlogAuthor);
                cmd.Parameters.AddWithValue("@BlogContent", newBlog.BlogContent);
                result = cmd.ExecuteNonQuery();
                message = result > 0 ? "Update Successful" : "Update Failed";
                Result:
                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public void Delete()
    {
        _connectionString = connectionServices.GetConnectionString();
        int blogId = 0;
        string message = string.Empty;
        int result = 0;
        try
        {
            Console.WriteLine("\n\nPlease type the Id of the record would like to Delete. Type 0 to return to main menu.\n\n");
            string commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nYou have to type an Id.\n");
                Delete();
            }
            Common common = new Common();
            blogId = int.Parse(commandInput);

            if (blogId == 0) common.GetUserCommand();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                Console.WriteLine("Connection Open. \n\n");

                #region 💕 Check Data 💕

                bool data = GetDataById(blogId);
                if (!data)
                {
                    message = "There is no Data!";
                    goto Result;
                }

                #endregion

                string query = CommonQuery.DeleteQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BlogId", blogId);
                result = cmd.ExecuteNonQuery();
                message = result > 0 ? "Delete Successful" : "Delete Failed";
                Result:
                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public bool SelectDataById()
    {
        int blogId = 0;
        _connectionString = connectionServices.GetConnectionString();
        try
        {
            Console.WriteLine("\n\nPlease type the Id of the record would like to Select. Type 0 to return to main menu.\n\n");
            string? commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nYou have to type an Id.\n");
                Delete();
            }
            Common common = new Common();
            blogId = int.Parse(commandInput);

            if (blogId == 0) common.GetUserCommand();

            using SqlConnection con = new(_connectionString);
            con.Open();
            Console.WriteLine("Connection Open");
            string query = CommonQuery.GetDataById;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@blogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0) return false;

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog Id => " + dr["BlogId"]);
                Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
                Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + dr["BlogContent"]);
            }
            Console.ReadLine();
            Console.Clear();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

        return true;
    }

    public bool GetDataById(int blogId)
    {
        _connectionString = connectionServices.GetConnectionString();
        try
        {
            using SqlConnection con = new(_connectionString);
            con.Open();
            Console.WriteLine("Connection Open. \n\n");
            string query = CommonQuery.GetDataById;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@blogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            if (dt.Rows.Count == 0) return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

        return true;
    }

    public BlogModel GetBlogData()
    {
        BlogModel newBlog = new BlogModel();

        try
        {
            while (string.IsNullOrEmpty(newBlog.BlogTitle))
            {
                Console.WriteLine("Please enter Blog Title");
                newBlog.BlogTitle = Console.ReadLine()!;
            }

            while (string.IsNullOrEmpty(newBlog.BlogAuthor))
            {
                Console.WriteLine("Please enter Blog Author");
                newBlog.BlogAuthor = Console.ReadLine()!;
            }

            while (string.IsNullOrEmpty(newBlog.BlogContent))
            {
                Console.WriteLine("Please enter Blog Content");
                newBlog.BlogContent = Console.ReadLine()!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

        return newBlog;
    }
}
