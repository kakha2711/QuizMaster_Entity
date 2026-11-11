
using Microsoft.EntityFrameworkCore;
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.DTOs;
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
            return GetAllPerson<T>().Result.FirstOrDefault(p => p.PersonalNumber == personalNumber);
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
                var studentUpdate = GetPersonByPersonalNumber<Student>(student.PersonalNumber).Result;

                var propertys = typeof(Student).GetProperty("Grade");

                studentUpdate.FirsName = person.FirsName != null ? person.FirsName : studentUpdate.FirsName;
                studentUpdate.Lastname = person.Lastname != null ? person.Lastname : studentUpdate.Lastname;
                studentUpdate.Grade = Convert.ToDouble(propertys.GetValue(person));
                studentUpdate.Email = person.Email != null ? person.Email : studentUpdate.Email;
                studentUpdate.PersonalNumber = person.PersonalNumber != null ? person.PersonalNumber : studentUpdate.PersonalNumber;
                studentUpdate.UserName = person.UserName != null ? person.UserName : studentUpdate.UserName;
                studentUpdate.Password = person.Password != null ? person.Password : studentUpdate.Password;
                studentUpdate.IsVerified = person.IsVerified != null ? person.IsVerified : studentUpdate.IsVerified;
                studentUpdate.IsDelete = person.IsDelete != null ? person.IsDelete : studentUpdate.IsDelete;
                studentUpdate.Role = person.Role != null ? person.Role : studentUpdate.Role;
                studentUpdate.Gender = person.Gender != null ? person.Gender : studentUpdate.Gender;


                _quiziMasterDb.Students.Update(studentUpdate);
            }
            else if (person is Lecturer lecturer)
            {
                

                var lecturerUpdate = GetPersonByPersonalNumber<Lecturer>(lecturer.PersonalNumber).Result;

                lecturerUpdate.FirsName = person.FirsName != null ? person.FirsName : lecturerUpdate.FirsName;
                lecturerUpdate.Lastname = person.Lastname != null ? person.Lastname : lecturerUpdate.Lastname;
                lecturerUpdate.Email = person.Email != null ? person.Email : lecturerUpdate.Email;
                lecturerUpdate.PersonalNumber = person.PersonalNumber != null ? person.PersonalNumber : lecturerUpdate.PersonalNumber;
                lecturerUpdate.UserName = person.UserName != null ? person.UserName : lecturerUpdate.UserName;
                lecturerUpdate.Password = person.Password != null ? person.Password : lecturerUpdate.Password;
                lecturerUpdate.IsVerified = person.IsVerified != null ? person.IsVerified : lecturerUpdate.IsVerified;
                lecturerUpdate.IsDelete = person.IsDelete != null ? person.IsDelete : lecturerUpdate.IsDelete;
                lecturerUpdate.Role = person.Role != null ? person.Role : lecturerUpdate.Role;
                lecturerUpdate.Gender = person.Gender != null ? person.Gender : lecturerUpdate.Gender;

                

                _quiziMasterDb.Lecturers.Update(lecturerUpdate);
            }

            return "Person updated successfully";
        }

        public async Task<string> DeletePerson<T>(T person) where T : Person
        {
            if(person is Student student)
            {
                UpdatePerson(person);
            }
            else if (person is Lecturer lecturer)
            {
                UpdatePerson(person);
            }

            return "Person deleted successfully";
        }

      
    }
}
