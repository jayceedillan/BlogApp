using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogApp.Infrastructure.DbContexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Build configuration and provide the connection string
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BlogApp.Web"))
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("blogDb"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
