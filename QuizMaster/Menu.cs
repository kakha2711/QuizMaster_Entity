using QuizMaster.Domane.Enum;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;
using QuizMaster.Service.Interface;
using Spectre.Console;
using System.Transactions;

namespace QuizMaster.UI
{
    internal static class Menu
    {

        public static async Task ShowMenuAsync(IPersonServices personServices, IQuestionServices questionServices)
        {
            var selected = AnsiConsole.Prompt(
               new MultiSelectionPrompt<string>()
               .Title("Which [green]features[/] do you want to enable?")
               .AddChoices("Lecturer", "Student"));

            AnsiConsole.WriteLine("You selected:");
            foreach (var item in selected)
            {
                AnsiConsole.WriteLine($"- {item}");
            }


            switch (selected[0])
            {
                case "Lecturer":
                    LogInOrRegisterMeniu(selected[0], personServices, questionServices);
                    break;
                case "Student":
                    LogInOrRegisterMeniu(selected[0], personServices, questionServices);
                    break;
            }

        }

        public static void LogInOrRegisterMeniu(string  role, IPersonServices personServices, IQuestionServices questionServices)
        {
            var selected = AnsiConsole.Prompt(
               new MultiSelectionPrompt<string>()
               .Title("Which [green]features[/] do you want to enable?")
               .AddChoices("Log In", "Register"));

            AnsiConsole.WriteLine("You selected:");
            foreach (var item in selected)
            {
                AnsiConsole.WriteLine($"- {item}");
            }

            switch (selected[0])
            {
                case "Register":
                    if (role == "Lecturer")
                        personServices.AddStudent(CreatePerson<LecturerDTO>(role));
                    else
                        personServices.AddStudent(CreatePerson<StudentDTO>(role));
                    break;
                case "Log In":
                    if (role == "Lecturer")
                        ShoWLecturerMeniu(personServices, questionServices);
                    else
                        ShoWStudentMeniu(personServices, questionServices);
                    break;
            }
        }

        public static async Task ShoWLecturerMeniu(IPersonServices personServices, IQuestionServices questionServices)
        {
            Console.Write("Enter User Name: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password:");
            string password = Console.ReadLine();

            LecturerDTO lecturer = personServices.LogIn<LecturerDTO>(username, password).Result;

            if(lecturer == null)
            {
                Console.WriteLine("Invalid username or password.");
                return;
            }

            var selected = AnsiConsole.Prompt(
                            new MultiSelectionPrompt<string>()
                           .Title("Which [green]features[/] do you want to enable?")
                           .AddChoices(
                                        "Show All Students",
                                        "Show Student Personul Number",
                                        "Show Lecturer",
                                        //"Show Lecturer by User Name",
                                        "Uppdate Leqturer",
                                        "Delete Leqturer",
                                        "Show All Tests",
                                        "Add Test",
                                        "Update Test",
                                        "Delete Test",
                                        "Show All Questions",
                                        "Add Question",
                                        "Update Question",
                                        "Delete Question",
                                        "Show All Answers",
                                        "Add Answer",
                                        "Update Answer",
                                        "Delete Answer"
                                        ));

            AnsiConsole.WriteLine("You selected:");
            foreach (var item in selected)
            {
                AnsiConsole.WriteLine($"- {item}");
            }

            //Console.WriteLine(selected[0]);

            switch(selected[0])
            {
                case "Show All Students":
                    Console.WriteLine();
                    var students = personServices.GetAllPersonService<StudentDTO>().Result;

                    foreach (var item in students)
                    {
                        Console.WriteLine(
                                            item.FirsName + "\n" +
                                            item.Lastname + "\n" +
                                            item.Email + "\n" +

                                            item.PhoneNumber + "\n" +
                                            item.PersonalNumber + "\n" +
                                            item.UserName + "\n" +
                                            item.Password + "\n" +
                                            item.VerificationCode + "\n" +
                                            item.IsDelete + "\n" +
                                            item.IsVerified + "\n" +
                                            item.Grade + "\n" +

                                            item.Gender + "\n" +
                                            item.Role
                                            );
                        Console.WriteLine();
                    }

                    Console.WriteLine();

                    break;

                case "Show Student Personul Number":

                    Console.WriteLine();
                    Console.Write("Enter Student Personal Number: ");
                    string personalNumber = Console.ReadLine();

                    var student = personServices.GetPersonByPersonalNumber<StudentDTO>(personalNumber).Result;

                    if (student == null)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }
                    Console.WriteLine(
                                            student.FirsName + "\n" +
                                            student.Lastname + "\n" +
                                            student.Email + "\n" +
                                            student.PhoneNumber + "\n" +
                                            student.PersonalNumber + "\n" +
                                            student.UserName + "\n" +
                                            student.Password + "\n" +
                                            student.VerificationCode + "\n" +
                                            student.IsDelete + "\n" +
                                            student.IsVerified + "\n" +
                                            student.Grade + "\n" +
                                            student.Gender + "\n" +
                                            student.Role
                                            );
                    Console.WriteLine();
                    break;

                case "Show Lecturer":

                    Console.WriteLine(
                                        "სახელი: " + lecturer.FirsName + "\n" +
                                        "გვარი: " + lecturer.Lastname + "\n" +
                                        "ფოსტა: " + lecturer.Email + "\n" +
                                        "ტელეფონი:" + lecturer.PhoneNumber + "\n" +
                                        "პირადი ნომერი: " + lecturer.PersonalNumber + "\n" +
                                        "იუზერნეიმი: " + lecturer.UserName + "\n" +
                                        "პაროლი: " + lecturer.Password + "\n" +
                                        "ვერიფიკაციის კოდი" + lecturer.VerificationCode + "\n" +
                                        "წაშლილი" + lecturer.IsDelete + "\n" +
                                        "ვერიფიცირებული" + lecturer.IsVerified + "\n" +
                                        "სქესი" + lecturer.Gender + "\n" +
                                        "როლი" + lecturer.Role
                                        );

                    break;
                case "Uppdate Leqturer":

                    //LecturerDTO lecturerDTO = new LecturerDTO
                    //{
                    //    FirsName = "კახაბერი",
                    //    Lastname = "სართანია",
                    //    Email = "kakhaberi.sartaniya@gmail.com",
                    //    PersonalNumber = "01011015058"
                    //};

                    lecturer.Lastname = "სართანია";
                    lecturer.Email = "kakhaberi.sartaniya@gmail.com";

                    personServices.UpdateStudent<LecturerDTO>(lecturer);
                    break;

                case "Add Test":

                    TestDTO testDTO = new TestDTO();
                    
                    Console.Write("Enter Test Title: ");
                    testDTO.Title = Console.ReadLine();

                    Console.Write("Enter Test Topic: ");
                    testDTO.Topic = Console.ReadLine();

                    Console.Write("Enter Test Questions Number: ");
                    testDTO.QuestionsNumber = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter Test Maximum Score: ");
                    testDTO.MaximumScore = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter Test Duration Minutes: ");
                    testDTO.DurationMinutes = Convert.ToByte(Console.ReadLine());

                    Console.Write("Enter Test Passing Percentage: ");
                    testDTO.PassingPercentage = Convert.ToDouble(Console.ReadLine());

                    string addQuestion = await questionServices.AddTest(testDTO);

                    break;
            }
        }


        public static void ShoWStudentMeniu(IPersonServices personServices, IQuestionServices questionServices)
            {

            Console.Write("Enter User Name: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password:");
            string password = Console.ReadLine();

            StudentDTO studentDTO = personServices.LogIn<StudentDTO>(username, password).Result;

            if(studentDTO == null)
            {
                Console.WriteLine("Invalid username or password.");
                return;
            }

            var selected = AnsiConsole.Prompt(
                            new MultiSelectionPrompt<string>()
            .               Title("Which [green]features[/] do you want to enable?")
                           .AddChoices(
                                        //"Show All Students",
                                        "Show Student",
                                        //"Show Student by User Name",
                                        //"Add Student",
                                        "Uppdate Student",
                                        "Delete Student",
                                        "Show Students Lider Boards"
                                        ));

            AnsiConsole.WriteLine("You selected:");
            foreach (var item in selected)
            {
                AnsiConsole.WriteLine($"- {item}");
            }

            //Console.WriteLine(selected[0]);

            switch (selected[0])
            {
                case "Show Student":

                        Console.WriteLine(
                                        "სახელი: " + studentDTO.FirsName + "\n" +
                                        "გვარი: " + studentDTO.Lastname + "\n" +
                                        "ფოსტა: " + studentDTO.Email + "\n" +
                                        "ტელეფონი:" + studentDTO.PhoneNumber + "\n" +
                                        "პირადი ნომერი: " + studentDTO.PersonalNumber + "\n" +
                                        "იუზერნეიმი: " + studentDTO.UserName + "\n" +
                                        "პაროლი: " + studentDTO.Password + "\n" +
                                        "ვერიფიკაციის კოდი" + studentDTO.VerificationCode + "\n" +
                                        "წაშლილი" + studentDTO.IsDelete + "\n" +
                                        "ვერიფიცირებული" + studentDTO.IsVerified + "\n" +
                                        "სქესი" + studentDTO.Gender + "\n" +
                                        "როლი" + studentDTO.Role
                                        );
                    

                    break;

                case "Uppdate Student":

                    studentDTO.FirsName = "მაია";
                    studentDTO.Lastname = "სარtანია";
                    studentDTO.Email = "sartania@gmail.com";
                    studentDTO.PersonalNumber = "99887744551";

                    personServices.UpdateStudent<StudentDTO>(studentDTO);

                    break;
            }
        }


        public static T CreatePerson<T>(string role) where T : PersonDTO
        {
            T persontDTO;

            if (role == "Student")
            {
                persontDTO = new StudentDTO() as T;

            }
            else
            {
                persontDTO = new LecturerDTO() as T;
            }


            Console.Write("Enter First Name: ");
            persontDTO.FirsName = Console.ReadLine();

            Console.Write("Enter LastName Name: ");
            persontDTO.Lastname = Console.ReadLine();

            Console.Write("Enter Email: ");
            persontDTO.Email = Console.ReadLine();

                Console.Write("Enter Phone Number: ");
            persontDTO.PhoneNumber = Console.ReadLine();


                Console.Write("Enter Personal Number: ");
            persontDTO.PersonalNumber = Console.ReadLine();


                Console.Write("Enter User Nume: ");
            persontDTO.UserName = Console.ReadLine();

                Console.Write("Enter Password: ");
            persontDTO.Password = Console.ReadLine();

            Console.Write("Enter Gender: ");
            persontDTO.Gender = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());

            //Console.Write("Enter LastName Name: ");
            persontDTO.Role = (Role)Enum.Parse(typeof(Role), role);

                


            return persontDTO;
        }

    }
}
