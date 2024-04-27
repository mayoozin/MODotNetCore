using Microsoft.EntityFrameworkCore;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AppDbContext : DbContext
    {
        private readonly DbConnectionServices _dbConnection;

        public AppDbContext(DbConnectionServices dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnection.GetConnectionString());
        }
        public DbSet<BlogModel> Blog { get; set; }
    }
}
