

namespace QuizMaster.Domane.Models.DTOs
{
    public class TestSubmissionsDTO
    {
        public int StudentId { get; set; }

        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public bool CorrectAnswer { get; set; } = false;

        public bool IsDelete { get; set; } = false;
    }
}
