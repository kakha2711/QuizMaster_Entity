
namespace QuizMaster.Domane.Models.DTOs
{
    public class AnswerDTO
    {
        public string? AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public bool isDelete { get; set; }
        public int QuestionTestId { get; set; }
    }
}
