using AutoMapper;
using Moq;
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Service.Services;

namespace QuizMaster.Test
{
    public class QuestionServicesTest
    {
        private readonly Mock<IQuestionRepository> _questionRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        private readonly QuestionServices _questionService;

        public QuestionServicesTest()
        {
            _questionRepository = new Mock<IQuestionRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _questionService = new QuestionServices(_questionRepository.Object, _mapper.Object, _unitOfWork.Object);
        }

        [Fact]
        public void AddTest_ShouldReturnSuccessMessage_WhenTestIsAdded()
        {
            // Arrange
            var testDTO = new TestDTO
            {
                Title = "Sample Test",
                Topic = "This is a sample test.",
                QuestionsNumber = 10,
                MaximumScore = 100,
                DurationMinutes = 60,
                PassingPercentage = 70
            };

            var test = new QuizMaster.Domane.Models.Entities.Test
            {
                Id = 1,
                Title = "Sample Test",
                Topic = "This is a sample test.",
                QuestionsNumber = 10,
                MaximumScore = 100,
                DurationMinutes = 60,
                PassingPercentage = 70
            };

            _mapper.Setup(m => m.Map<QuizMaster.Domane.Models.Entities.Test>(testDTO)).Returns(test);

            _questionService.AddTest(testDTO);

            _questionRepository.Verify(k => k.AddTest(It.Is<QuizMaster.Domane.Models.Entities.Test>(m =>
                            m.Title == testDTO.Title &&
                            m.Topic == testDTO.Topic &&
                            m.QuestionsNumber == testDTO.QuestionsNumber &&
                            m.MaximumScore == testDTO.MaximumScore &&
                            m.DurationMinutes == testDTO.DurationMinutes &&
                            m.PassingPercentage == testDTO.PassingPercentage)));

            _unitOfWork.Verify(p => p.SaveChanges(), Times.Once);

            
        }




        //ეს ტესტი უნდა შეამოწმოს UpdateTest მეთოდი, რომ სწორად მუშაობს და აბრუნებს წარმატების შეტყობინებას.
        [Fact]
        public async Task UpdateTest_Should_Update_And_Return_Success_Message()
        {
            // Arrange
            var existingTest = new QuizMaster.Domane.Models.Entities.Test
            {
                Id = 1,
                Title = "Math Test",
                Topic = "Algebra",
                QuestionsNumber = 10,
                MaximumScore = 100,
                DurationMinutes = 30,
                PassingPercentage = 60,
                IsDelete = false
            };

            var testDto = new TestDTO
            {
                Title = "Updated Math Test",
                Topic = "Advanced Algebra",
                QuestionsNumber = 15,
                MaximumScore = 150,
                DurationMinutes = 45,
                PassingPercentage = 70,
                IsDelete = false
            };

            // ვამზადებთ Repository-ს mock-ს, რომ GetAllTests დააბრუნოს ჩვენი ტესტური მონაცემი
            // შენიშვნა: თუ GetAllTests ასინქრონულია, გამოიყენეთ შესაბამისი მეთოდი (მაგ: Task.FromResult)
            // აქ ვიმფელურებთ, რომ GetAllTests() მუშაობს სინქრონულად ან ბრუნდება Task<IEnumerable<Test>>
            _questionRepository
                .Setup(r => r.GetAllTests())
                .ReturnsAsync(new List<QuizMaster.Domane.Models.Entities.Test> { existingTest });

            var mappedEntity = new QuizMaster.Domane.Models.Entities.Test { Id = 1, Title = "Updated Math Test" };

            _mapper
                .Setup(m => m.Map<QuizMaster.Domane.Models.Entities.Test>(It.IsAny<object>()))
                .Returns(mappedEntity);

            _questionRepository
    .Setup(r => r.UpdateTest(It.IsAny<QuizMaster.Domane.Models.Entities.Test>()))
    .ReturnsAsync("Test updated successfully.");

            //_questionRepository
            //    .Setup(r => r.UpdateTest(It.IsAny<QuizMaster.Domane.Models.Entities.Test>()))
            //    .Returns(Task.CompletedTask);

            //_questionRepository
            //    .Setup(r => r.UpdateTest(It.IsAny<QuizMaster.Domane.Models.Entities.Test>()))
            //    .Returns(Task.CompletedTask);

            // Act
            var result = await _questionService.UpdateTest(testDto);

            // Assert
            Assert.Equal("Test updated successfully", result);

            _questionRepository.Verify(r => r.UpdateTest(It.IsAny<QuizMaster.Domane.Models.Entities.Test>()), Times.Once);
        }


    }
}
