using QuizMaster.Domane.Enum;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;
using QuizMaster.Service.Interface;
using Spectre.Console;

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

        public static void LogInOrRegisterMeniu(string role, IPersonServices personServices, IQuestionServices questionServices)
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

            if (lecturer == null)
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
                                        "Uppdate Leqturer",
                                        "Delete Leqturer",
                                        "Show All Tests",
                                        "Add Test",
                                        "Update Test",
                                        "Delete Test",
                                        "Show All Questions",
                                        "Add Question and Answers",
                                        "Update Question",
                                        "Delete Question",
                                        "Show All Answers",
                                        "Update Answer",
                                        "Delete Answer"
                                        ));

            AnsiConsole.WriteLine("You selected:");
            foreach (var item in selected)
            {
                AnsiConsole.WriteLine($"- {item}");
            }

            switch (selected[0])
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


                case "\"Add Question and Answers":

                    QuestionDTO questionDTO = new QuestionDTO();
                    AnswerDTO answerTests;

                    int count = 0;
                    double questionNumber = 0;

                    do
                    {
                        Console.Write("Enter Question Text: ");
                        questionDTO.QuestionText = Console.ReadLine();

                        var Tests = await questionServices.GetAllTests();

                        var selectedQuestion = AnsiConsole.Prompt(
                                   new SelectionPrompt<TestDTO>()
                                       .Title("Select a [green]book[/]")
                                       .PageSize(10)
                                       .UseConverter(Tests => $"{Tests.Id} by{Tests.Title}")
                                       .AddChoices(Tests));

                        AnsiConsole.MarkupLine($"You selected: [yellow]{selectedQuestion.Title}[/]");

                        questionDTO.TestId = selectedQuestion.Id;

                        var fruit = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                               .Title("Enter ChoiceQuestion:")
                               .AddChoices("SingleChoiceQuestion", "MultiChoiceQuestion"));

                        AnsiConsole.MarkupLine($"You selected: [green]{fruit}[/]");
                        questionDTO.ChoiceQuestion = (ChoiceQuestion)Enum.Parse(typeof(ChoiceQuestion), fruit);


                        questionNumber = selectedQuestion.QuestionsNumber;
                        count++;

                        string addQuestionResult = await questionServices.AddQuestion(questionDTO);

                        List<QuestionDTO> questionDTOs = await questionServices.GetAllQuestions();

                        int index = questionDTOs.FirstOrDefault(q => q.QuestionText == questionDTO.QuestionText).Id;

                        for (int i = 0; i < 4; i++)
                        {
                            answerTests = new AnswerDTO();

                            Console.Write($"Enter {i + 1} Answer: ");
                            answerTests.AnswerText = Console.ReadLine();

                            Console.Write("Enter IsCorrect: ");

                            string? answer = AnsiConsole.Prompt(
                                 new SelectionPrompt<string>()
                                .Title("Enter IsCorrect:")
                                .AddChoices("True", "False"));

                            AnsiConsole.MarkupLine($"You selected: [green]{answer}[/]");

                            bool isvalis = bool.TryParse(answer, out bool result);

                            answerTests.IsCorrect = result;
                            answerTests.QuestionTestId = index;

                            string addAnswersResult = await questionServices.AddAnswer(answerTests);
                        }
                    }
                    while (count < questionNumber);

                    break;



                case "add Answer":

                    AnswerDTO answerDTO = new AnswerDTO();

                    Console.Write("Enter Answer Text: ");
                    answerDTO.AnswerText = Console.ReadLine();

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

            if (studentDTO == null)
            {
                Console.WriteLine("Invalid username or password.");
                return;
            }

            var selected = AnsiConsole.Prompt(
                            new MultiSelectionPrompt<string>()
            .Title("Which [green]features[/] do you want to enable?")
                           .AddChoices(
                                        "Show Student",
                                        //"Add Student",
                                        "Uppdate Student",
                                        "Show Students Lider Boards",
                                        "start Test"
                                        ));

            AnsiConsole.WriteLine("You selected:");
            foreach (var item in selected)
            {
                AnsiConsole.WriteLine($"- {item}");
            }

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

                case "start Test":

                    StudentTestResultDTO studentTestResultDTO = new StudentTestResultDTO();
                    TestSubmissionsDTO testSubmissionsDTO = new TestSubmissionsDTO();

                    var starrtDate = DateTime.Now;

                    studentTestResultDTO.StudentId = studentDTO.Id;
                    studentTestResultDTO.StartDate = starrtDate;

                    testSubmissionsDTO.StudentId = studentDTO.Id;


                    var Tests = questionServices.GetAllTests().Result;

                    var selectedQuestion = AnsiConsole.Prompt(
                                  new SelectionPrompt<TestDTO>()
                                      .Title("Select a [green]book[/]")
                                      .PageSize(10)
                                      .UseConverter(Tests => $"{Tests.Id} by{Tests.Title}")
                                      .AddChoices(Tests));

                    AnsiConsole.MarkupLine($"You selected: [yellow]{selectedQuestion.Title}[/]");

                    var indexTest = selectedQuestion.Id;

                    Random random = new Random();

                    List<QuestionDTO> questions = questionServices
                                                    .GetAllQuestions().Result
                                                    .Where(q => q.TestId == indexTest)
                                                    .OrderBy(q => random.Next())
                                                    .Take(5)
                                                    .ToList();

                    double qula = selectedQuestion.MaximumScore/ selectedQuestion.QuestionsNumber;

                    double sumScore = 1;

                    int count = 0;

                    foreach (var question in questions)
                    {
                        AnsiConsole.WriteLine($"Question: {question.QuestionText}");

                        var answers = questionServices.GetAllAnswer().Result.Where(a => a.QuestionTestId == question.Id).ToList();

                        List<string> selected0 = AnsiConsole.Prompt(
                            new MultiSelectionPrompt<string>()
                            .Title("Select [green]notification plugins[/] to install:")
                            .AddChoices(answers.Select(a => $"{a.Id}: {a.AnswerText}")));

                        AnsiConsole.MarkupLine($"[blue]Installing {selected.Count} plugin(s)...[/]");

                        string[] strings = new string[selected0.Count];

                        for (int i = 0; i < selected0.Count; i++)
                        {
                            int answerId = Convert.ToInt32(selected0[i].Split(':')[0]);

                            var answer = answers.FirstOrDefault(a => a.Id == answerId);

                            if (answer != null && answer.IsCorrect)
                            {
                                count++;
                            }

                            testSubmissionsDTO.AnswerId = answerId;
                            testSubmissionsDTO.QuestionId = question.Id;
                            testSubmissionsDTO.CorrectAnswer = answer.IsCorrect;

                            questionServices.AddTestSubmissions(testSubmissionsDTO);
                        }

                        if (count == selected0.Count)
                        {
                            sumScore += 2 * qula;
                        }
                            count = 0;
                    }

                    studentTestResultDTO.Score = sumScore;
                    studentTestResultDTO.TestId = indexTest;


                    questionServices.AddTestResults(studentTestResultDTO);

                   studentDTO.Grade = sumScore;

                    personServices.UpdateStudent<StudentDTO>(studentDTO);//ეს არის შესამოწმებელი

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
