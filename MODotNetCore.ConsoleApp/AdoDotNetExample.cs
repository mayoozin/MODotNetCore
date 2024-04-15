using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp
{
    internal class AdoDotNetExample
    {
        SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "DESKTOP-QIPPQBI\\SQLEXPRESS",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sasa@123"
        };

        public void Read()
        {

            using (SqlConnection con = new(_sqlConnectionStringBuilder.ConnectionString))
            {
                con.Open();
                Console.WriteLine("Connection Open");

                string query = CommonQuery.SelectQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("Blog Id => " + dr["BlogId"]);
                    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
                    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
                }
            }
        }

        public void Create()
        {
            string title = "Heal The World";
            string author = "Jsamine Dylan";
            string content = @"12th April marks the International Day for Human Space Flight, observed worldwide. 
This day commemorates a significant milestone in human history:";
            int result = 0;
            using (SqlConnection con = new(_sqlConnectionStringBuilder.ConnectionString))
            {
                con.Open();
                Console.WriteLine("Connection Open");
                string query = CommonQuery.CreateQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BlogTitle", title);
                cmd.Parameters.AddWithValue("@BlogAuthor", author);
                cmd.Parameters.AddWithValue("@BlogContent", content);
                result = cmd.ExecuteNonQuery();

                con.Close();
            }

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

        public void Update()
        {
            string blogId = "1";
            string title = "International Day of Human Space";
            string author = "David Mamama";
            string content = @"12th April marks the International Day for Human Space Flight, observed worldwide. 
This day commemorates a significant milestone in human history:";
            string message = string.Empty;
            int result = 0;
            using (SqlConnection con = new(_sqlConnectionStringBuilder.ConnectionString))
            {
                con.Open();
                Console.WriteLine("Connection Open");

                #region 💕 Check Data 💕

                bool data = GetDataById(blogId);
                if (!data)
                {
                    message = "There is no Data!";
                    goto Result;
                }

                #endregion

                string query = CommonQuery.UpdateQuery;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BlogId", blogId);
                cmd.Parameters.AddWithValue("@BlogTitle", title);
                cmd.Parameters.AddWithValue("@BlogAuthor", author);
                cmd.Parameters.AddWithValue("@BlogContent", content);
                result = cmd.ExecuteNonQuery();
                con.Close();
            }

            message = result > 0 ? "Update Successful" : "Update Failed";
            Result:
            Console.WriteLine(message);
        }

        public void Delete()
        {
            string blogId = "1";
            string message = string.Empty;
            int result = 0;
            using (SqlConnection con = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                con.Open();
                Console.WriteLine("Connection Open");

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

                con.Close();
            }

            message = result > 0 ? "Delete Successful" : "Delete Failed";
            Result:
            Console.WriteLine(message);
        }

        public bool GetDataById(string blogId)
        {
            using SqlConnection con = new(_sqlConnectionStringBuilder.ConnectionString);
            con.Open();
            Console.WriteLine("Connection Open");
            string query = CommonQuery.GetDataById;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@blogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            if (dt.Rows.Count == 0) return false;

            return true;
        }
    }
}
