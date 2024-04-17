using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.Services
{
    public class DbConnectionServices
    {
        public string GetConnectionString()
        {
            SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
            {
                DataSource = "DESKTOP-QIPPQBI\\SQLEXPRESS",
                InitialCatalog = "DotNetTrainingBatch4",
                UserID = "sa",
                Password = "sasa@123"
            };
            return _sqlConnectionStringBuilder.ConnectionString;
        }

    }
}
