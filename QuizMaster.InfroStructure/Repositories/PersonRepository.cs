
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.Entities;
using QuizMaster.InfroStructure.Data;

namespace QuizMaster.InfroStructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly QuizMasterDbContext _quiziMasterDb;
        
        public PersonRepository(QuizMasterDbContext quizMasterDb)
        {
            _quiziMasterDb = quizMasterDb;
        }

        public Task<List<Person>> GetAllStudent(string role)
        {
            List<Student> students = new List<Student>();
            List<Lecturer> lecturers = new List<Lecturer>();


            if (role == "Leqturer")
                lecturers = _quiziMasterDb.Lecturers.ToList();
            else
                students = _quiziMasterDb.Students.ToList();


            return lecturers;
        }

        public Person GetPersonByPersonalNumber(string personalNumber, string role)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonByUserName(string username, string role)
        {
            throw new NotImplementedException();
        }

        public Task<string> AddStudent(Person person, int param = 0)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateStudent(Person student)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteStudent(string personalNumber)
        {
            throw new NotImplementedException();
        }
    }
}
