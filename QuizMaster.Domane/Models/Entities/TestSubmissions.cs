
namespace QuizMaster.Domane.Models.Entities
{
    public class TestSubmissions
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }

        public bool CorrectAnswer { get; set; } = false;

        public bool IsDelete { get; set; } = false;
    }
}
