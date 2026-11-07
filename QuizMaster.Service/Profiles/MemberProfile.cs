
using AutoMapper;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.Service.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Lecturer, LecturerDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<StudentTestResult, StudentTestResultDTO>().ReverseMap();
            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<TestSubmissions, TestSubmissionsDTO>().ReverseMap();
        }
    }
}
