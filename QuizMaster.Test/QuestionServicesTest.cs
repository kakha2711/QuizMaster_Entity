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

            // GetAllTests() მეთოდის დასამაპინგებელი DTO
            var existingTestDTO = new TestDTO
            {
                Title = "Math Test",
                Topic = "Algebra",
                QuestionsNumber = 10,
                MaximumScore = 100,
                DurationMinutes = 30,
                PassingPercentage = 60,
                IsDelete = false
            };

            // განახლებული მონაცემები
            var testDTO = new TestDTO
            {
                Title = "Math Test",
                Topic = "Advanced Algebra",
                QuestionsNumber = 15,
                MaximumScore = 150,
                DurationMinutes = 45,
                PassingPercentage = 70,
                IsDelete = false
            };

            var mappedEntity = new QuizMaster.Domane.Models.Entities.Test
            {
                Id = 1,
                Title = "Math Test",
                Topic = "Advanced Algebra",
                QuestionsNumber = 15,
                MaximumScore = 150,
                DurationMinutes = 45,
                PassingPercentage = 70,
                IsDelete = false
            };

            // GetAllTests Repository
            _questionRepository
                .Setup(r => r.GetAllTests())
                .ReturnsAsync(new List<QuizMaster.Domane.Models.Entities.Test>
                {
            existingTest
                });

            // Entity -> DTO
            _mapper
                .Setup(m => m.Map<List<TestDTO>>(
                    It.IsAny<List<QuizMaster.Domane.Models.Entities.Test>>()))
                .Returns(new List<TestDTO> { existingTestDTO });

            // DTO -> Entity
            _mapper
                .Setup(m => m.Map<QuizMaster.Domane.Models.Entities.Test>(
                    It.IsAny<TestDTO>()))
                .Returns(mappedEntity);

            _questionRepository
                .Setup(r => r.UpdateTest(
                    It.IsAny<QuizMaster.Domane.Models.Entities.Test>()))
                .ReturnsAsync("Test updated successfully.");

            // Act
            var result = await _questionService.UpdateTest(testDTO);

            // Assert
            Assert.Equal("Test updated successfully", result);

            _questionRepository.Verify(
                r => r.UpdateTest(It.Is<QuizMaster.Domane.Models.Entities.Test>(t =>
                    t.Title == testDTO.Title &&
                    t.Topic == testDTO.Topic &&
                    t.QuestionsNumber == testDTO.QuestionsNumber &&
                    t.MaximumScore == testDTO.MaximumScore &&
                    t.DurationMinutes == testDTO.DurationMinutes &&
                    t.PassingPercentage == testDTO.PassingPercentage
                )),
                Times.Once);
        }



        [Theory]
        [InlineData(1, "Math Test", "Algebra", 10, 100, (byte)30, 60)]
        [InlineData(2, "CSharp Test", "CSharp Basics", 15, 150, (byte)45, 70)]
        [InlineData(3, "SQL Test", "SQL Fundamentals", 20, 200, (byte)60, 80)]
        [InlineData(4, "OOP Test", "Object Oriented Programming", 25, 250, (byte)90, 90)]
        public async Task UpdateTest_Should_Update_And_Return_Success_Message_With_Different_Parameters(
    int id,
    string title,
    string topic,
    int questionsNumber,
    int maximumScore,
    byte durationMinutes,
    int passingPercentage)
        {
            // Arrange

            var existingTest = new QuizMaster.Domane.Models.Entities.Test
            {
                Id = id,
                Title = title,
                Topic = topic,
                QuestionsNumber = questionsNumber,
                MaximumScore = maximumScore,
                DurationMinutes = durationMinutes,
                PassingPercentage = passingPercentage,
                IsDelete = false
            };

            var existingTestDTO = new TestDTO
            {
                Title = title,
                Topic = topic,
                QuestionsNumber = questionsNumber,
                MaximumScore = maximumScore,
                DurationMinutes = durationMinutes,
                PassingPercentage = passingPercentage,
                IsDelete = false
            };

            var testDTO = new TestDTO
            {
                Title = title,
                Topic = "Updated " + topic,
                QuestionsNumber = questionsNumber + 5,
                MaximumScore = maximumScore + 50,
                DurationMinutes = (byte)(durationMinutes + 15),
                PassingPercentage = passingPercentage + 5,
                IsDelete = false
            };

            var mappedEntity = new QuizMaster.Domane.Models.Entities.Test
            {
                Id = id,
                Title = title,
                Topic = testDTO.Topic,
                QuestionsNumber = testDTO.QuestionsNumber,
                MaximumScore = testDTO.MaximumScore,
                DurationMinutes = testDTO.DurationMinutes,
                PassingPercentage = testDTO.PassingPercentage,
                IsDelete = false
            };

            _questionRepository
                .Setup(r => r.GetAllTests())
                .ReturnsAsync(new List<QuizMaster.Domane.Models.Entities.Test>
                {
            existingTest
                });

            _mapper
                .Setup(m => m.Map<List<TestDTO>>(
                    It.IsAny<List<QuizMaster.Domane.Models.Entities.Test>>()))
                .Returns(new List<TestDTO> { existingTestDTO });

            _mapper
                .Setup(m => m.Map<QuizMaster.Domane.Models.Entities.Test>(
                    It.IsAny<TestDTO>()))
                .Returns(mappedEntity);

            _questionRepository
                .Setup(r => r.UpdateTest(
                    It.IsAny<QuizMaster.Domane.Models.Entities.Test>()))
                .ReturnsAsync("Test updated successfully.");

            // Act
            var result = await _questionService.UpdateTest(testDTO);

            // Assert
            Assert.Equal("Test updated successfully", result);

            _questionRepository.Verify(
                r => r.UpdateTest(It.Is<QuizMaster.Domane.Models.Entities.Test>(t =>
                    t.Id == id &&
                    t.Title == title &&
                    t.Topic == testDTO.Topic &&
                    t.QuestionsNumber == testDTO.QuestionsNumber &&
                    t.MaximumScore == testDTO.MaximumScore &&
                    t.DurationMinutes == testDTO.DurationMinutes &&
                    t.PassingPercentage == testDTO.PassingPercentage
                )),
                Times.Once);
        }


     

    //    [Theory]
    //    [InlineData(1, "Test 1", "Topic 1", 10, 100, 60, 70)]
    //    //[InlineData(new TestDTO { Title = "Test 1", Topic = "Topic 1", QuestionsNumber = 10, MaximumScore = 100, DurationMinutes = 60, PassingPercentage = 70, IsDelete = false })]
    //    public async Task UpdateTest_Should_Update_And_Return_Success_Message_With_Different_Parameters(int id, string title, string topic, int questionsNumber, int maximumScore, byte durationMinutes, int passingPercentage)
    //    //public async Task UpdateTest_Should_Update_And_Return_Success_Message_With_Different_Parameters(TestDTO testDTO)

    //    {
    //        // Arrange
    //        var existingTest = new QuizMaster.Domane.Models.Entities.Test
    //        {
    //            Id = id,
    //            Title = title,
    //            Topic = topic,
    //            QuestionsNumber = questionsNumber,
    //            MaximumScore = maximumScore,
    //            DurationMinutes = durationMinutes,
    //            PassingPercentage = passingPercentage,
    //            IsDelete = false
    //        };

    //        var testDto = new TestDTO
    //        {
    //            Title = "Updated " + title,
    //            Topic = "Updated " + topic,
    //            QuestionsNumber = questionsNumber + 5,
    //            MaximumScore = maximumScore + 50,
    //            DurationMinutes = (byte)(durationMinutes + 15),
    //            PassingPercentage = passingPercentage + 10,
    //            IsDelete = false
    //        };

    //        // ვამზადებთ Repository-ს mock-ს, რომ GetAllTests დააბრუნოს ჩვენი ტესტური მონაცემი
    //        _questionRepository
    //            .Setup(r => r.GetAllTests())
    //            .ReturnsAsync(new List<QuizMaster.Domane.Models.Entities.Test> { existingTest });

    //        var mappedEntity = new QuizMaster.Domane.Models.Entities.Test { Id = id, Title = "Updated " + title };

    //        _mapper
    //            .Setup(m => m.Map<QuizMaster.Domane.Models.Entities.Test>(It.IsAny<object>()))
    //            .Returns(mappedEntity);

    //        _questionRepository
    //.Setup(r => r.UpdateTest(It.IsAny<QuizMaster.Domane.Models.Entities.Test>()))
    //.ReturnsAsync("Test updated successfully.");

    //        // Act
    //        var result = await _questionService.UpdateTest(testDto);

    //        // Assert
    //        Assert.Equal("Test updated successfully", result);

    //        _questionRepository.Verify(r => r.UpdateTest(It.IsAny<QuizMaster.Domane.Models.Entities.Test>()), Times.Once);
    //    }
    }
}
