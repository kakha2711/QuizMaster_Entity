
using QuizMaster.Domane.Enum;

namespace QuizMaster.Domane.Models.DTOs
{
    public class PersonDTO
    {
        public string? FirsName { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PersonalNumber { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? VerificationCode { get; set; }
        public bool IsVerified { get; set; } = false;
        public bool IsDelete { get; set; } = false;

        public Role Role { get; set; }
        public Gender Gender { get; set; }
    }
}
