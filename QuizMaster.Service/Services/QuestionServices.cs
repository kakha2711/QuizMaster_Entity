
using AutoMapper;
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;
using QuizMaster.Service.Interface;

namespace QuizMaster.Service.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionServices(IQuestionRepository questionRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        //---------------------Start Test----------------------------

        public async Task<List<TestDTO>> GetAllTests()
        {
            List<Test> tests = await _questionRepository.GetAllTests();

            var testDTOs = _mapper.Map<List<TestDTO>>(tests);

            return await Task.FromResult(testDTOs);
        }

        public async Task<Test> GetTestById(int id)
        {
            Test tets = await _questionRepository.GetTestById(id);

            var testDTOs = _mapper.Map<Test>(tets);

            return await Task.FromResult(testDTOs);
        }

        public async Task<string> AddTest(TestDTO testCatalog)
        {
            var testEntity = _mapper.Map<Test>(testCatalog);

            await _questionRepository.AddTest(testEntity);

            return await Task.FromResult("Test added successfully");
        }

        public async Task<string> UpdateTest(TestDTO testCatalog)
        {
            var getTest = GetAllTests().Result.FirstOrDefault(t => t.Title == testCatalog.Title);

            getTest.Title = testCatalog.Title == null || testCatalog.Title.Equals(getTest.Title) ? getTest.Title : testCatalog.Title;
            getTest.Topic = testCatalog.Topic == null || testCatalog.Topic.Equals(getTest.Topic) ? getTest.Topic : testCatalog.Topic;
            getTest.QuestionsNumber = testCatalog.QuestionsNumber == 0 || testCatalog.QuestionsNumber == getTest.QuestionsNumber ? getTest.QuestionsNumber : testCatalog.QuestionsNumber;
            getTest.MaximumScore = testCatalog.MaximumScore == 0 || testCatalog.MaximumScore == getTest.MaximumScore ? getTest.MaximumScore : testCatalog.MaximumScore;
            getTest.DurationMinutes = testCatalog.DurationMinutes == 0 || testCatalog.DurationMinutes == getTest.DurationMinutes ? getTest.DurationMinutes : testCatalog.DurationMinutes;
            getTest.PassingPercentage = testCatalog.PassingPercentage == 0 || testCatalog.PassingPercentage == getTest.PassingPercentage ? getTest.PassingPercentage : testCatalog.PassingPercentage;
            getTest.IsDelete = testCatalog.IsDelete == getTest.IsDelete ? getTest.IsDelete : testCatalog.IsDelete;


            var testEntity = _mapper.Map<Test>(getTest);

            

            await _questionRepository.UpdateTest(testEntity);

            return await Task.FromResult("Test updated successfully");
        }

        public async Task<string> DeleteTest(TestDTO testCatalog)
        {
            var testEntity = _mapper.Map<Test>(testCatalog);

            await _questionRepository.DeleteTest(testEntity);

            return await Task.FromResult("Test deleted successfully");
        }
        //---------------------End Test----------------------------

        //---------------------Start Question----------------------------

        public async Task<List<QuestionDTO>> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestions();

            var questionDTOs = _mapper.Map<List<QuestionDTO>>(questions);

            return await Task.FromResult(questionDTOs);
        }

        public async Task<List<QuestionDTO>> GetAllQuestionsById(int id)
        {
            var questions = await _questionRepository.GetAllQuestionsById(id);

            var questionDTOs = _mapper.Map<List<QuestionDTO>>(questions);

            return await Task.FromResult(questionDTOs);
        }

        public async Task<List<QuestionDTO>> GetQuestionsByTestId(int testCatalogId)
        {
            var questions = await _questionRepository.GetQuestionsByTestId(testCatalogId);

            var questionDTOs = _mapper.Map<List<QuestionDTO>>(questions);

            return await Task.FromResult(questionDTOs);
        }

        public async Task<string> AddQuestion(QuestionDTO questionTest)
         {
            var questionEntity = _mapper.Map<Question>(questionTest);
            await _questionRepository.AddQuestion(questionEntity);
            return await Task.FromResult("Question added successfully");
         }

        //ზემოთ რაც არის გასასწორებელია აიდები აკილი მის მიხედვით ხდება მოძებნა

        public async Task<string> UpdateQuestion(QuestionDTO questionTest)
        {
            var getQuestion = GetAllQuestions().Result.FirstOrDefault(q => q.Id == questionTest.Id);

            questionTest.QuestionText = questionTest.QuestionText == null || questionTest.QuestionText.Equals(getQuestion.QuestionText) ? getQuestion.QuestionText : questionTest.QuestionText;
            questionTest.IsDelete = questionTest.IsDelete == getQuestion.IsDelete ? getQuestion.IsDelete : questionTest.IsDelete;
            questionTest.ChoiceQuestion = questionTest.ChoiceQuestion == getQuestion.ChoiceQuestion ? getQuestion.ChoiceQuestion : questionTest.ChoiceQuestion;
            questionTest.TestId = questionTest.TestId == 0 || questionTest.TestId == getQuestion.TestId ? getQuestion.TestId : questionTest.TestId;

            var questionEntity = _mapper.Map<Question>(questionTest);

            _questionRepository.UpdateQuestion(questionEntity);

            return await Task.FromResult("Question updated successfully");
        }

        public async Task<string> DeleteQuestion(QuestionDTO questionTest)
        {
            questionTest.IsDelete = true;

            var question = _mapper.Map<Question>(questionTest);
            _questionRepository.DeleteQuestion(question);

            return await Task.FromResult("Question deleted successfully");
        }

        //---------------------End Question----------------------------

        //---------------------Start Answer----------------------------

        public async Task<List<AnswerDTO>> GetAllAnswer()
        {
            var answers = await _questionRepository.GetAllAnswer();

            return _mapper.Map<List<AnswerDTO>>(answers);
        }

        public async Task<AnswerDTO> GetAnswerById(int Id)
        {
            var answer = await _questionRepository.GetAnswerById(Id);

            return _mapper.Map<AnswerDTO>(answer);
        }

        public async Task<List<AnswerDTO>> GetAnswerByQuestionId(int questionId)
        {
            var answers = await _questionRepository.GetAnswerByQuestionId(questionId);

            return _mapper.Map<List<AnswerDTO>>(answers);
        }

        public async Task<string> AddAnswer(AnswerDTO answerTest)
        {
            var answer = _mapper.Map<Answer>(answerTest);

            await _questionRepository.AddAnswer(answer);

            return await Task.FromResult("Answer added successfully");
        }

        public async Task<string> UpdateAnswer(AnswerDTO answerTest)
        {
            var ansver = GetAnswerById(answerTest.Id).Result;

            answerTest.AnswerText = answerTest.AnswerText == null || answerTest.AnswerText.Equals(ansver.AnswerText) ? ansver.AnswerText : answerTest.AnswerText;
            answerTest.IsCorrect = answerTest.IsCorrect == ansver.IsCorrect ? ansver.IsCorrect : answerTest.IsCorrect;
            answerTest.isDelete = answerTest.isDelete == ansver.isDelete ? ansver.isDelete : answerTest.isDelete;
            answerTest.QuestionTestId = answerTest.QuestionTestId == 0 || answerTest.QuestionTestId == ansver.QuestionTestId ? ansver.QuestionTestId : answerTest.QuestionTestId;


            var answer = _mapper.Map<Answer>(answerTest);
            await _questionRepository.UpdateAnswer(answer);
            return await Task.FromResult("Answer updated successfully");
        }

        public async Task<string> DeleteAnswer(AnswerDTO answerTest)
        {
            var answer = _mapper.Map<Answer>(answerTest);
            await _questionRepository.DeleteAnswer(answer);
            return await Task.FromResult("Answer deleted successfully");
        }
        //---------------------End Answer----------------------------

        //---------------------Start Test Submissions----------------------------

        public async Task<List<TestSubmissionsDTO>> GetAllTestSubmissions(int Studentid)
        {
            var testSubmissions = await _questionRepository.GetAllTestSubmissions(Studentid);
            return _mapper.Map<List<TestSubmissionsDTO>>(testSubmissions);
        }

        public async Task<List<TestSubmissionsDTO>> GetTestSubmissionsDTObyStudentId(int studentId)
        {
            var testSubmissions = await _questionRepository.GetTestSubmissionsDTObyStudentId(studentId);
            return _mapper.Map<List<TestSubmissionsDTO>>(testSubmissions);
        }

        public async Task<string> AddTestSubmissions(TestSubmissionsDTO testSubmissions)
        {
            var testSubmissionsEntity = _mapper.Map<TestSubmissions>(testSubmissions);

            await _questionRepository.AddTestSubmissions(testSubmissionsEntity);

            return await Task.FromResult("Test submission added successfully.");
        }

        //---------------------End Test Submissions----------------------------

        //---------------------Start Test Results----------------------------

        public async Task<StudentTestResultDTO> AddTestResults(StudentTestResultDTO testResult)
        {
            var testResultEntity = _mapper.Map<StudentTestResult>(testResult);

            await _questionRepository.AddTestResults(testResultEntity);

            return await Task.FromResult(_mapper.Map<StudentTestResultDTO>(testResultEntity));
        }

     
        public async Task<List<StudentTestResultDTO>> GetTestResults()
        {
            var testResults = await _questionRepository.GetTestResults();
            return _mapper.Map<List<StudentTestResultDTO>>(testResults);
        }

        //---------------------End Test Results----------------------------

    }
}
