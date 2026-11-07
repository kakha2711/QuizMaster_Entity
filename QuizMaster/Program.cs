
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizMaster.Domane.Interface;
using QuizMaster.InfroStructure.Data;
using QuizMaster.InfroStructure.Repositories;

namespace QuizMaster
{
    internal class Program
    {
        
        static void Main(string[] args)
        {



            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json").Build();


            string connectionString = config.GetConnectionString("DefaultConnection");


            var services = new ServiceCollection();

            services.AddDbContext<QuizMasterDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            var serviceProvider = services.BuildServiceProvider();

        }
    }
}






