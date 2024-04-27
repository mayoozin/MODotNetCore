using Microsoft.EntityFrameworkCore;
using MODotNetCore.RestApi.Model;
using MODotNetCore.RestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.RestApi.EfDbContext
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.connection.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
