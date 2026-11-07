
using FluentValidation;

namespace QuizMaster.Domane.Models.Entities
{
    public class Test
    {
        public Test()
        {
            var validator = new TestValidator();
            var validationResult = validator.Validate(this);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? Topic { get; set; }
        public double QuestionsNumber { get; set; }
        public double MaximumScore { get; set; }
        public byte DurationMinutes { get; set; }
        public double PassingPercentage { get; set; }//გამსვლელი პროცენტი
        public bool IsDelete { get; set; } = false;

        public ICollection<Question> Questions { get; set; }
        public ICollection<StudentTestResult> StudentTestResults { get; set; }
    }

    public class TestValidator : AbstractValidator<Test>
    {
        public TestValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("TestTitle is required.");
            RuleFor(x => x.Topic).NotEmpty().WithMessage("Topic is required.");
            RuleFor(x => x.QuestionsNumber).InclusiveBetween(0, double.MaxValue).WithMessage("QuestionsNumber must be greater than or equal to 0.");
            RuleFor(x => x.MaximumScore).InclusiveBetween(0, double.MaxValue).WithMessage("MaximumScore must be greater than or equal to 0.");
            RuleFor(x => x.DurationMinutes).GreaterThan((byte)0).WithMessage("DurationMinutes must be greater than 0.");
            RuleFor(x => x.PassingPercentage).InclusiveBetween(0, 100).WithMessage("PassingPercentage must be between 0 and 100.");
        }
    }
}
