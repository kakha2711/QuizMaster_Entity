
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.Service.Interface
{
    public interface IPersonServices
    {
        public Task<List<T>> GetAllPersonService<T>() where T : PersonDTO;
        public Task<T> GetPersonByPersonalNumber<T>(string personalNumber) where T : PersonDTO;
        public  Task<T> GetPersonByUserName<T>(string username) where T : PersonDTO;
        public  Task<string> AddStudent<T>(T person) where T : PersonDTO;
        public Task<string> UpdateStudent<T>(T person) where T : PersonDTO;
        public Task<string> DeleteStudent<T>(T person) where T : PersonDTO;


    }
}
