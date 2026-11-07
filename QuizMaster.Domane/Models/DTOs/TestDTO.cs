
namespace QuizMaster.Domane.Models.DTOs
{
    public class TestDTO
    {
        public string Title { get; set; }
        public string? Topic { get; set; }
        public double QuestionsNumber { get; set; }
        public double MaximumScore { get; set; }
        public byte DurationMinutes { get; set; }
        public double PassingPercentage { get; set; }//გამსვლელი პროცენტი
        public bool IsDelete { get; set; } = false;
    }
}
