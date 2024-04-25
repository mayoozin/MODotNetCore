using MODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp
{
    public class DapperExample
    {
        DbConnectionServices connectionServices = new DbConnectionServices();
        string? _connectionString;
        public void Run()
        {
            Read();
        }

        public void Read()
        {
            _connectionString = connectionServices.GetConnectionString();

        }
    }
}
