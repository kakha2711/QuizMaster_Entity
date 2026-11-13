
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.Service.Interface
{
    public interface IQuestionServices
    {
        //for Test
        Task<List<TestDTO>> GetAllTests();
        Task<Test> GetTestById(int id);
        Task<string> AddTest(Test testCatalog);
        Task<string> UpdateTest(Test testCatalog);
        Task<string> DeleteTest(Test testCatalog);



        //for Question
        Task<List<Question>> GetAllQuestions();
        Task<List<Question>> GetAllQuestionsById(int id);
        Task<List<Question>> GetQuestionsByTestId(int testCatalogId);
        Task<string> AddQuestion(Question questionTest);
        Task<string> UpdateQuestion(Question questionTest);
        Task<string> DeleteQuestion(Question questionTest);

        //for Ansver
        Task<List<Answer>> GetAllAnswer();
        Task<Answer> GetAnswerById(int Studentid);
        Task<List<Answer>> GetAnswerByQuestionId(int questionId);
        Task<string> AddAnswer(Answer answerTest);
        Task<string> UpdateAnswer(Answer answerTest);
        Task<string> DeleteAnswer(Answer answerTest);


        //for Test Submissions
        Task<List<TestSubmissions>> GetAllTestSubmissions(int Studentid);
        Task<List<TestSubmissions>> GetTestSubmissionsDTObyStudentId(int studentId);
        Task<string> AddTestSubmissions(TestSubmissions testSubmissions);

        //for Test Results
        Task<List<StudentTestResult>> GetTestResults();
        Task<StudentTestResult> AddTestResults(StudentTestResult testResult);
    }
}
