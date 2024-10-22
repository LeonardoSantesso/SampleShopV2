using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.Factory
{
    public class SampleShopV2ContextFactory : IDesignTimeDbContextFactory<SampleShopV2Context>
    {
        public SampleShopV2Context CreateDbContext(string[] args)
        {
            // Define the path to the other project's appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "SampleShopV2"))
                .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SampleShopV2Context>();

            // Configure the DbContext to use SQL Server
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new SampleShopV2Context(optionsBuilder.Options);
        }
    }
}