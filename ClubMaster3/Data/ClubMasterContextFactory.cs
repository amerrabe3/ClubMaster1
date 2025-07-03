using ClubMaster3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ClubMaster3.Data
{
    public class ClubMasterContextFactory : IDesignTimeDbContextFactory<ClubMaster3Context>
    {
        public ClubMaster3Context CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // أو full path إذا بدك
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ClubMasterConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ClubMaster3Context>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ClubMaster3Context(optionsBuilder.Options);
        }
    }
}
