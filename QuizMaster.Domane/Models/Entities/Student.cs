using FluentValidation;

namespace QuizMaster.Domane.Models.Entities
{
    public class Student : Person
    {
       

        public double Grade { get; set; }

        public ICollection<StudentTestResult> StudentTestResults { get; set; }
        public ICollection<TestSubmissions> TestSubmissions { get; set; }
    }

    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Grade)
                .InclusiveBetween(0, 100)
                .WithMessage("Grade must be between 0 and 100.");
        }
    }
}


