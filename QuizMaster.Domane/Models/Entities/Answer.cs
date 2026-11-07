
using System.ComponentModel.DataAnnotations;

namespace QuizMaster.Domane.Models.Entities
{
    public class Answer
    {
        
        public int Id { get; set; }
        public string? AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public bool isDelete { get; set; }
        public int QuestionTestId { get; set; }
        public Question Question { get; set; }

        public ICollection<TestSubmissions> TestSubmissions { get; set; }
    }
}
