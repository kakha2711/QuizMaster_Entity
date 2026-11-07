
namespace QuizMaster.Domane.Models.DTOs
{
    public class StudentTestResultDTO
    {
        public DateTime StartDate { get; set; }
        public double Score { get; set; }
        public bool IsDelete { get; set; } = false;

        public int StudentId { get; set; }
        public int TestId { get; set; }
    }
}
