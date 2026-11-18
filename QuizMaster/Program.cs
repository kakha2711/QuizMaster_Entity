
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.InfroStructure.Data;
using QuizMaster.InfroStructure.Repositories;
using QuizMaster.Service.Interface;
using QuizMaster.Service.Services;
using QuizMaster.UI;
using Spectre.Console;

namespace QuizMaster
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;


            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json").Build();


            string connectionString = config.GetConnectionString("DefaultConnection");




            var services = new ServiceCollection();

            services.AddLogging();

            services.AddDbContext<QuizMasterDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonServices, PersonServices>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionServices, QuestionServices>();


            services.AddAutoMapper(ap =>
            {
                ap.AddMaps(typeof(Program).Assembly);
                ap.AddMaps(typeof(PersonServices).Assembly);
            });

            var serviceProvider = services.BuildServiceProvider();

            IPersonServices personServices = serviceProvider.GetRequiredService<IPersonServices>();
            IQuestionServices questionServices = serviceProvider.GetRequiredService<IQuestionServices>();

            Menu.ShowMenuAsync(personServices, questionServices).Wait();
        }
    }
}


