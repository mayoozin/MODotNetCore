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

            SqlConnection con = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            con.Open();
            Console.WriteLine("Connection Open");

            #region Read

            string query = "Select * From Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            #endregion

            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Blog Id => " + dr["BlogId"]);
                Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
                Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content => " + dr["BlogContent"]);
            }
        }

        public void Create()
        {
            string title = "International Day of Human Space";
            string author = "David";
            string content = @"12th April marks the International Day for Human Space Flight, observed worldwide. 
This day commemorates a significant milestone in human history:";
            SqlConnection con = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            con.Open();
            Console.WriteLine("Connection Open");
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
                    (@BlogTitle,
                    @BlogAuthor,
                    @BlogContent)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            con.Close();
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
            SqlConnection con = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            con.Open();
            Console.WriteLine("Connection Open");
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            con.Close();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }
    }
}
