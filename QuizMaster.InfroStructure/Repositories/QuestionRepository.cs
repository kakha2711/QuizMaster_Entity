using QuizMaster.Domane.Interface;
//using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;
using QuizMaster.InfroStructure.Data;


namespace QuizMaster.InfroStructure.Repositories
{
    internal class QuestionRepository : IQuestionRepository
    {
        private readonly QuizMasterDbContext _quizMasterDb;

        public QuestionRepository(QuizMasterDbContext quizMasterDb)
        {
            _quizMasterDb = quizMasterDb;
        }

        //---------------------Start Test----------------------------

        public async Task<List<Test>> GetAllTests()
        {
            List<Test> tests = _quizMasterDb.Tests.ToList();

            return await Task.FromResult(tests);
        }

        public async Task<Test> GetTestById(int id)
        {
            Test? test = _quizMasterDb.Tests.FirstOrDefault(t => t.Id == id);

            if(test == null)
            {
                throw new Exception($"Test with ID {id} not found.");
            }

            return await Task.FromResult(test);
        }

        public async Task<string> AddTest(Test testCatalog)
        {
            _quizMasterDb.Tests.Add(testCatalog);

            return await Task.FromResult("Test added successfully.");
        }

        public async Task<string> UpdateTest(Test testCatalog)
        {
            var testUpdate = _quizMasterDb.Tests.Update(testCatalog);

            return await Task.FromResult("Test updated successfully.");
        }

        public async Task<string> DeleteTest(Test testCatalog)
        {
            var tt = UpdateTest(testCatalog);

            return await Task.FromResult("Test deleted successfully.");
        }

        //---------------------End Test----------------------------




        //---------------------Start Question----------------------------

        public async Task<List<Question>> GetAllQuestions()
        {
            return await Task.FromResult(_quizMasterDb.Questions.ToList());
        }

        public async Task<List<Question>> GetAllQuestionsById(int id)
        {
            return await Task.FromResult(_quizMasterDb.Questions.Where(q => q.Id == id).ToList());
        }

        public async Task<List<Question>> GetQuestionsByTestId(int testCatalogId)
        {
            return await Task.FromResult(_quizMasterDb.Questions.Where(q => q.TestId == testCatalogId).ToList());
        }

        public async Task<string> AddQuestion(Question questionTest)
        {
            var question = _quizMasterDb.Questions.Add(questionTest);

            return await Task.FromResult("Question added successfully.");
        }


        public async Task<string> UpdateQuestion(Question questionTest)
        {
            var question = _quizMasterDb.Questions.Update(questionTest);

            return await Task.FromResult("Question updated successfully.");
        }

        public async Task<string> DeleteQuestion(Question questionTest)
        {
            var question = _quizMasterDb.Questions.Remove(questionTest);

            return await Task.FromResult("Question deleted successfully.");
        }

        //---------------------End Question----------------------------



        //---------------------Start Ansver----------------------------

        public async Task<List<Answer>> GetAllAnswer()
        {
            return await Task.FromResult(_quizMasterDb.Answers.ToList());
        }

        public async Task<Answer> GetAnswerById(int Id)
        {
            var answer = _quizMasterDb.Answers.FirstOrDefault(a => a.Id == Id);

            //aq Id shevcvale da sxvaganac shesacvlelia

            if(answer == null)
            {
                throw new Exception($"Answer with ID {Id} not found.");
            }
            return await Task.FromResult(answer);
        }

        public async Task<List<Answer>> GetAnswerByQuestionId(int questionId)
        {
            var answers = _quizMasterDb.Answers.Where(a => a.QuestionTestId == questionId).ToList();

            return await Task.FromResult(answers);
        }

        public async Task<string> AddAnswer(Answer answerTest)
        {
            var answer = _quizMasterDb.Answers.Add(answerTest);

            return await Task.FromResult("Answer added successfully.");
        }

        public async Task<string> UpdateAnswer(Answer answerTest)
        {
            var answer = _quizMasterDb.Answers.Update(answerTest);

            return await Task.FromResult("Answer updated successfully.");
        }

        public async Task<string> DeleteAnswer(Answer answerTest)
        {
            var answer = _quizMasterDb.Answers.Remove(answerTest);

            return await Task.FromResult("Answer deleted successfully.");
        }

        //---------------------End Ansver----------------------------




        //---------------------Start TestResult----------------------------

        public async Task<List<StudentTestResult>> GetTestResults()
        {
            return await Task.FromResult(_quizMasterDb.StudentTestResults.ToList());
        }

        public async Task<StudentTestResult> AddTestResults(StudentTestResult testResult)
        {
            var testResultEntity = _quizMasterDb.StudentTestResults.Add(testResult);

            return await Task.FromResult(testResultEntity.Entity);
        }



        //---------------------Start TestResult----------------------------



        //---------------------Start TestResult----------------------------

        public async Task<List<TestSubmissions>> GetTestSubmissionsDTObyStudentId(int studentId)
        {
            return await Task.FromResult(_quizMasterDb.TestSubmissions.Where(t => t.StudentId == studentId).ToList());
        }

        public async Task<List<TestSubmissions>> GetAllTestSubmissions(int Studentid)
        {
            return await Task.FromResult(_quizMasterDb.TestSubmissions.Where(t => t.StudentId == Studentid).ToList());
        }

        public async Task<string> AddTestSubmissions(TestSubmissions testSubmissions)
        {
            var testSubmissionsEntity = _quizMasterDb.TestSubmissions.Add(testSubmissions);

            return await Task.FromResult("Test submission added successfully.");
        }


        //---------------------Start ----------------------------




    }
}
