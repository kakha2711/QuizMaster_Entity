
using Microsoft.EntityFrameworkCore;
using QuizMaster.InfroStructure.Data;
using QuizMaster.InfroStructure.Repositories;

namespace QuizMaster.Test
{
    public class QuestionRepozitory
    {
       

        public QuizMasterDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<QuizMasterDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new QuizMasterDbContext(options);
        }


        [Fact]
        public async Task AddTest()
        {
            // Arrange
            var context = CreateContext();
            var repository = new QuestionRepository(context);


            var testEntity = new QuizMaster.Domane.Models.Entities.Test
            {
                Id = 1,
                Title = "Sample Test",
                Topic = "This is a sample test.",
                QuestionsNumber = 10,
                MaximumScore = 100,
                DurationMinutes = 60,
                PassingPercentage = 70

            };

            repository.AddTest(testEntity);
            context.SaveChanges();

            var addTest = context.Tests.Find(1);
            Assert.NotNull(addTest);
            Assert.Equal("Sample Test", addTest.Title);
            Assert.Equal("This is a sample test.", addTest.Topic);
            Assert.Equal(10, addTest.QuestionsNumber);
            Assert.Equal(100, addTest.MaximumScore);
            Assert.Equal(60, addTest.DurationMinutes);
            Assert.Equal(70, addTest.PassingPercentage);
            
        }
    }
}
