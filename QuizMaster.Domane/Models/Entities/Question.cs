
using FluentValidation;
using QuizMaster.Domane.Enum;

namespace QuizMaster.Domane.Models.Entities
{
    public class Question
    {
        public Question()
        {
            var validator = new QuestionTestValidator();
            var result = validator.Validate(this);
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }

        public bool IsDelete { get; set; } = false;
        public ChoiceQuestion ChoiceQuestion { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<TestSubmissions> TestSubmissions { get; set; }
    }

    public class QuestionTestValidator : AbstractValidator<Question>
    {
        public QuestionTestValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Question is required.");
            RuleFor(x => x.IsDelete).IsInEnum().WithMessage("IsCorrectis true or false.");
            RuleFor(x => x.ChoiceQuestion).IsInEnum().WithMessage("ChoiceQuestion must be a valid enum value.");
            RuleFor(x => x.TestId).InclusiveBetween(0, int.MaxValue).WithMessage("Id must be greater than 0.");
            //RuleFor(x=>x.test).NotNull().WithMessage("Test is required.");

        }
    }
}
