
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



        public async Task<List<T>> GetAllPerson<T>() where T : Person
        {
            if (typeof(T) == typeof(Lecturer))
            {
                return _quiziMasterDb.Lecturers.Cast<T>().ToList();
            }

            if (typeof(T) == typeof(Student))
            {
                return _quiziMasterDb.Students.Cast<T>().ToList();
            }

            return default;
        }



        public async Task<T> GetPersonByPersonalNumber<T>(string personalNumber ) where T : Person
        {
            return GetAllPerson<Person>().Result.Cast<T>().FirstOrDefault(p => p.PersonalNumber == personalNumber);
        }

        public async Task<T> GetPersonByUserName<T>(string username) where T : Person
        {
            return await Task.FromResult(GetAllPerson<T>().Result.Cast<T>().FirstOrDefault(p => p.UserName == username));
        }

        public async Task<string> AddStudent<T>(T person) where T : Person
        {
            string errorMessage = string.Empty;

            if (person is Student student)
            {
                var tt = _quiziMasterDb.Students.Add(student);
            }
            else if (person is Lecturer lecturer)
            {
                try
                {
                    _quiziMasterDb.Lecturers.Add(lecturer);
                }
                catch (Exception ex)
                {
                    var tt = ex.Message;
                }
            }

            return "Student added successfully";
        }

        public async Task<string> UpdatePerson<T>(T person) where T : Person
        {

            if(person is Student student)
            {
                _quiziMasterDb.Students.Update(student);
            }
            else if (person is Lecturer lecturer)
            {
                _quiziMasterDb.Lecturers.Update(lecturer);
            }

            return "Person updated successfully";
        }

        public async Task<string> DeletePerson<T>(T person) where T : Person
        {
            if(person is Student student)
            {
                person.IsDelete = true;
                UpdatePerson(person);
            }
            else if (person is Lecturer lecturer)
            {
                person.IsDelete = true;
                UpdatePerson(person);
            }

            return "Person deleted successfully";
        }

      
    }
}
