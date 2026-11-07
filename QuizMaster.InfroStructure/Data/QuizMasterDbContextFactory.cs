using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace QuizMaster.InfroStructure.Data
{
    public class QuizMasterDbContextFactory
         : IDesignTimeDbContextFactory<QuizMasterDbContext>
    {
        public QuizMasterDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder =
                new DbContextOptionsBuilder<QuizMasterDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new QuizMasterDbContext(optionsBuilder.Options);
        }
    }
}



