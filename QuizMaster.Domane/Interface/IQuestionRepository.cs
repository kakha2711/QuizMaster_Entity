
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.Domane.Interface
{
    public interface IQuestionRepository
    {
        //for student
        Task<List<Test>> GetAllTests();
        Task<Test> GetTestById(int id);
        Task<string> AddTest(Test testCatalog);
        Task<string> UpdateTest(Test testCatalog);
        Task<string> DeleteTest(Test testCatalog);



        //for Lecturer
        Task<List<Question>> GetAllQuestions();
        Task<List<Question>> GetAllQuestionsById(int id);
        Task<List<Question>> GetQuestionsByTestId(int testCatalogId);
        Task<string> AddQuestion(Question questionTest);
        Task<string> UpdateQuestion(Question questionTest);
        Task<string> DeleteQuestion(Question questionTest);

        
        Task<List<Answer>> GetAllAnswer();
        Task<Answer> GetAnswerById(int Studentid);
        Task<List<Answer>> GetAnswerByQuestionId(int questionId);
        Task<string> AddAnswer(Answer answerTest);
        Task<string> UpdateAnswer(Answer answerTest);
        Task<string> DeleteAnswer(Answer answerTest);



        Task<List<TestSubmissions>> GetAllTestSubmissions(int Studentid);
        Task<List<TestSubmissions>> GetTestSubmissionsDTObyStudentId(int studentId);
        Task<string> AddTestSubmissions(TestSubmissions testSubmissions);


        Task<List<StudentTestResult>> GetTestResults();
        Task<StudentTestResult> AddTestResults(StudentTestResult testResult);


    }
}
