namespace MODotNetCore.RestApiWithNLayer.Db
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
