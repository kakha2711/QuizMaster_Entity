
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.Service.Interface
{
    public interface IQuestionServices
    {
        //for Test
        Task<List<TestDTO>> GetAllTests();
        Task<TestDTO> GetTestById(int id);
        Task<string> AddTest(TestDTO testCatalog);
        Task<string> UpdateTest(TestDTO testCatalog);
        Task<string> DeleteTest(TestDTO testCatalog);



        //for Question
        Task<List<QuestionDTO>> GetAllQuestions();
        Task<List<QuestionDTO>> GetAllQuestionsById(int id);
        Task<List<QuestionDTO>> GetQuestionsByTestId(int testCatalogId);
        Task<string> AddQuestion(QuestionDTO questionTest);
        Task<string> UpdateQuestion(QuestionDTO questionTest);
        Task<string> DeleteQuestion(QuestionDTO questionTest);

        //for Ansver
        Task<List<AnswerDTO>> GetAllAnswer();
        Task<AnswerDTO> GetAnswerById(int Studentid);
        Task<List<AnswerDTO>> GetAnswerByQuestionId(int questionId);
        Task<string> AddAnswer(AnswerDTO answerTest);
        Task<string> UpdateAnswer(AnswerDTO answerTest);
        Task<string> DeleteAnswer(AnswerDTO answerTest);


        //for Test Submissions
        Task<List<TestSubmissionsDTO>> GetAllTestSubmissions(int Studentid);
        Task<List<TestSubmissionsDTO>> GetTestSubmissionsDTObyStudentId(int studentId);
        Task<string> AddTestSubmissions(TestSubmissionsDTO testSubmissions);

        //for Test Results
        Task<List<StudentTestResultDTO>> GetTestResults();
        Task<StudentTestResultDTO> AddTestResults(StudentTestResultDTO testResult);
    }
}
