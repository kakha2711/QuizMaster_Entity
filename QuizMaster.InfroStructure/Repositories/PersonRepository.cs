
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.InfroStructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Task<string> AddStudent(Person person, int param = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteStudent(string personalNumber)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetAllStudent(string role)
        {
            throw new NotImplementedException();
        }

        public Person GetPersonByPersonalNumber(string personalNumber, string role)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonByUserName(string username, string role)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateStudent(Person student)
        {
            throw new NotImplementedException();
        }
    }
}
