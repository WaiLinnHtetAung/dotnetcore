using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=.;Initial Catalog=dotnet;User ID=sa;Password=saPassword1234;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}