
using QuizMaster.Domane.Enum;

namespace QuizMaster.Domane.Models.Entities
{
    public class StudentTestResult
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public double Score { get; set; }
        public bool IsDelete { get; set; } = false;

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public CompletedOngoingTests CompletedOngoingTests { get; set; }
    }
}
