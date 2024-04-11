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
        public void Read()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.DataSource = "DESKTOP-QIPPQBI\\SQLEXPRESS";
            stringBuilder.InitialCatalog = "DotNetTrainingBatch4";
            stringBuilder.UserID = "sa";
            stringBuilder.Password = "sasa@123";
            SqlConnection con = new SqlConnection(stringBuilder.ConnectionString);
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
    }
}
