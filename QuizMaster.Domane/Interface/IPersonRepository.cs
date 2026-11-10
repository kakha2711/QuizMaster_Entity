
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.Domane.Interface
{
    public interface IPersonRepository
    {
        public Task<List<T>> GetAllPerson<T>() where T : Person;
        public Task<T> GetPersonByPersonalNumber<T>(string personalNumber) where T : Person;
        public Task<T> GetPersonByUserName<T>(string username) where T : Person;
        public Task<string> AddStudent<T>(T person) where T : Person;
        public Task<string> UpdatePerson<T>(T student) where T : Person;
        public Task<string> DeletePerson<T>(T person) where T : Person;
    }
}
