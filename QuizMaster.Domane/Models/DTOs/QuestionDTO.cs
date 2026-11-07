
using QuizMaster.Domane.Enum;

namespace QuizMaster.Domane.Models.DTOs
{
    public class QuestionDTO
    {
        public bool IsDelete { get; set; } = false;
        public ChoiceQuestion ChoiceQuestion { get; set; }
        public int TestId { get; set; }
    }
}
