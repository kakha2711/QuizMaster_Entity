
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.InfroStructure.Data;
using QuizMaster.InfroStructure.Repositories;
using QuizMaster.Service.Interface;
using QuizMaster.Service.Services;

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

            services.AddLogging();

            services.AddDbContext<QuizMasterDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonServices, PersonServices>();


            services.AddAutoMapper(ap =>
            {
                ap.AddMaps(typeof(Program).Assembly);
                ap.AddMaps(typeof(PersonServices).Assembly);
            });

            var serviceProvider = services.BuildServiceProvider();

            var personServices = serviceProvider.GetRequiredService<IPersonServices>();

            #region Add lecturer



            LecturerDTO lecturer = new LecturerDTO
            {
                FirsName = "კახაბერ",
                Lastname = "სართანია",
                Email = "sartania.ka@gmail.com",
                PhoneNumber = "599985226",
                PersonalNumber = "01011015058",
                UserName = "sartusha80",
                Password = "sartusha1",

                Role = Domane.Enum.Role.Lecturer,
                Gender = Domane.Enum.Gender.Male
            };

            //LecturerDTO lecturer = new LecturerDTO
            //{
            //    FirsName = "ani",
            //    Lastname = "კაჭკაჭიშვილი",
            //    Email = "kochi12@gmail.com",
            //    PhoneNumber = "577487898",
            //    PersonalNumber = "99887744552",
            //    UserName = "kacho12",
            //    Password = "kacho12",

            //    Role = Domane.Enum.Role.Lecturer,
            //    Gender = Domane.Enum.Gender.Female
            //};

            //StudentDTO studentDTO = new StudentDTO
            //{
            //    FirsName = "გიორგი",
            //    Lastname = "ბერიძე",
            //    Email = "beridze.giorgi@gmail.com",
            //    PhoneNumber = "555123456",
            //    PersonalNumber = "11223344553",
            //    UserName = "beridze12",
            //    Password = "beridze12",
            //    Grade = 10,

            //    Role = Domane.Enum.Role.Student,
            //    Gender = Domane.Enum.Gender.Male
            //};

            //personServices.AddStudent(studentDTO).Wait();

            #endregion

            #region Get person

            var students = personServices.GetAllPersonService<StudentDTO>().Result;

            var lecturers = personServices.GetAllPersonService<LecturerDTO>().Result;

            foreach (var item in students)
            {
                Console.WriteLine(item.FirsName + " " + item.Lastname + " " + item.Grade + " " + item.Gender);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in lecturers)
            {
                Console.WriteLine(item.FirsName + " " + item.Lastname + " " + item.Gender);
            }

            #endregion




        }
    }
}






