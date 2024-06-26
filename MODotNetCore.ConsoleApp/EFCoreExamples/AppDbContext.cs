﻿using Microsoft.EntityFrameworkCore;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Services;

namespace MODotNetCore.ConsoleApp.EFCoreExamples;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStringServices.connection.ConnectionString);
    }
    public DbSet<BlogModel> Blogs { get; set; }
}
