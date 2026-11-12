
using AutoMapper;
using BCrypt.Net;
using QuizMaster.Domane.Interface;
using QuizMaster.Domane.Models.DTOs;
using QuizMaster.Domane.Models.Entities;
using QuizMaster.Service.Interface;



namespace QuizMaster.Service.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PersonServices(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<T>> GetAllPersonService<T>() where T: PersonDTO
        {
            if (typeof(T) == typeof(StudentDTO))
            {
                List<Student> persons = _personRepository.GetAllPerson<Student>().Result;

                var personsDto = _mapper.Map<List<StudentDTO>>(persons).Cast<T>().ToList();

                return personsDto;
                
            }
            else if ( typeof(T) == typeof(LecturerDTO))
            {
                List<Lecturer> persons = _personRepository.GetAllPerson<Lecturer>().Result;

                var personsDto = _mapper.Map<List<LecturerDTO>>(persons).Cast<T>().ToList();

                return personsDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetPersonByPersonalNumber<T>(string personalNumber) where T : PersonDTO
        {
            if (typeof(T) == typeof(StudentDTO))
            {

                Student student = await _personRepository.GetPersonByPersonalNumber<Student>(personalNumber);

                var studentDTO = _mapper.Map<StudentDTO>(student) as T;

                return studentDTO;
            }
            else if (typeof(T) == typeof(LecturerDTO))
            {
                Lecturer lecturer = await _personRepository.GetPersonByPersonalNumber<Lecturer>(personalNumber);
                var lecturerDTO = _mapper.Map<LecturerDTO>(lecturer) as T;

                return lecturerDTO;
            }
            else
            {
                return default;
            }
        }

        public async Task<T> GetPersonByUserName<T>(string username) where T : PersonDTO
        {
            

            if (typeof(T) == typeof(StudentDTO))
            {
                Student student = await _personRepository.GetPersonByUserName<Student>(username);

                var studentDTO = _mapper.Map<StudentDTO>(student) as T;

                return studentDTO;
            }
            else if (typeof(T) == typeof(LecturerDTO))
            {
                Lecturer lecturer = await _personRepository.GetPersonByUserName<Lecturer>(username);

                var lecturerDTO = _mapper.Map<LecturerDTO>(lecturer) as T;
                
                return lecturerDTO;
              
            }
            else
            {
                return default;
            }
        }

        public async Task<string> AddStudent<T>(T person) where T : PersonDTO
        {
            Random random = new Random();
            int verificationCode = random.Next(1000, 9999);

            if (typeof(T) == typeof(StudentDTO))
            {

                Student student = _mapper.Map<Student>(person);
                
                student.VerificationCode = verificationCode.ToString();
                student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
                student.PhoneNumber = "+995" + student.PhoneNumber;

                //var personEntity = _mapper.Map<Person>(person);
                var result = await _personRepository.AddStudent(student);

                _unitOfWork.SaveChanges();

                return "Student added successfully";
            }
            else if (typeof(T) == typeof(LecturerDTO))
            {
                try
                {
                    Lecturer lecturer = _mapper.Map<Lecturer>(person);

                    lecturer.VerificationCode = verificationCode.ToString();
                    lecturer.Password = BCrypt.Net.BCrypt.HashPassword(lecturer.Password);
                    lecturer.PhoneNumber = "+995" + lecturer.PhoneNumber;

                    var result = await _personRepository.AddStudent(lecturer);

                    _unitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Handle the exception or log it
                    var tt = $"Error adding lecturer: {ex.Message}";

                    var yy = ex.InnerException.Message;
                }

                return "Lecturer added successfully";

            }
            else
            {
                return default;
            }

            
        }

        public async Task<string> UpdateStudent<T>(T person) where T : PersonDTO
        {
            string result = string.Empty;

            if (typeof(T) == typeof(StudentDTO))
            {

                var studentUpdate = GetPersonByPersonalNumber<StudentDTO>(person.PersonalNumber).Result;

                var propertys = typeof(Student).GetProperty("Grade");

                studentUpdate.FirsName = (person.FirsName != null || studentUpdate.FirsName != person.FirsName) ? person.FirsName : studentUpdate.FirsName;
                studentUpdate.Lastname = (person.Lastname != null || studentUpdate.Lastname != person.Lastname) ? person.Lastname : studentUpdate.Lastname;

                studentUpdate.Grade = Convert.ToDouble(propertys.GetValue(person));

                studentUpdate.Email = (person.Email != null || studentUpdate.Email != person.Email) ? person.Email : studentUpdate.Email;
                studentUpdate.PersonalNumber = (person.PersonalNumber != null || studentUpdate.PersonalNumber != person.PersonalNumber) ? person.PersonalNumber : studentUpdate.PersonalNumber;
                studentUpdate.UserName = (person.UserName != null || studentUpdate.UserName != person.UserName) ? person.UserName : studentUpdate.UserName;
                studentUpdate.Password = (person.Password != null || studentUpdate.Password != person.Password) ? person.Password : studentUpdate.Password;
                studentUpdate.IsVerified = (person.IsVerified != null || studentUpdate.IsVerified != person.IsVerified) ? person.IsVerified : studentUpdate.IsVerified;
                studentUpdate.IsDelete = (person.IsDelete != null || studentUpdate.IsDelete != person.IsDelete) ? person.IsDelete : studentUpdate.IsDelete;
                studentUpdate.PhoneNumber = (person.PhoneNumber != null || person.PhoneNumber != studentUpdate.PhoneNumber) ? person.PhoneNumber : studentUpdate.PhoneNumber;
                studentUpdate.VerificationCode = (person.VerificationCode != null || person.VerificationCode != studentUpdate.VerificationCode) ? person.VerificationCode : studentUpdate.VerificationCode;
                studentUpdate.Role = (person.Role != null || studentUpdate.Role != person.Role) ? person.Role : studentUpdate.Role;
                studentUpdate.Gender = (person.Gender != null || studentUpdate.Gender != person.Gender) ? person.Gender : studentUpdate.Gender;

                var personEntity = _mapper.Map<Student>(studentUpdate);
                
                result = await _personRepository.UpdatePerson(personEntity);

                _unitOfWork.SaveChanges();
            }
            else if (typeof(T) == typeof(LecturerDTO))
            {
                var lecturerUpdate = GetPersonByPersonalNumber<LecturerDTO>(person.PersonalNumber).Result;

                lecturerUpdate.FirsName = (person.FirsName != null || person.FirsName != lecturerUpdate.FirsName ) ? person.FirsName : lecturerUpdate.FirsName;
                lecturerUpdate.Lastname = (person.Lastname != null || person.Lastname != lecturerUpdate.Lastname) ? person.Lastname : lecturerUpdate.Lastname;
                lecturerUpdate.Email = (person.Email != null || person.Email !=lecturerUpdate.Email) ? person.Email : lecturerUpdate.Email;
                lecturerUpdate.PersonalNumber = (person.PersonalNumber != null || person.PersonalNumber != lecturerUpdate.PersonalNumber) ? person.PersonalNumber : lecturerUpdate.PersonalNumber;
                lecturerUpdate.UserName = (person.UserName != null || person.UserName != lecturerUpdate.UserName) ? person.UserName : lecturerUpdate.UserName;
                lecturerUpdate.Password = (person.Password != null || person.Password != lecturerUpdate.Password) ? person.Password : lecturerUpdate.Password;
                lecturerUpdate.IsVerified = (person.IsVerified != null || person.IsVerified != lecturerUpdate.IsVerified) ? person.IsVerified : lecturerUpdate.IsVerified;
                lecturerUpdate.IsDelete = (person.IsDelete != null || person.IsDelete != lecturerUpdate.IsDelete) ? person.IsDelete : lecturerUpdate.IsDelete;
                lecturerUpdate.PhoneNumber = (person.PhoneNumber != null || person.PhoneNumber != lecturerUpdate.PhoneNumber) ? person.PhoneNumber : lecturerUpdate.PhoneNumber;
                lecturerUpdate.VerificationCode = (person.VerificationCode != null || person.VerificationCode != lecturerUpdate.VerificationCode) ? person.VerificationCode : lecturerUpdate.VerificationCode;
                lecturerUpdate.Role = (person.Role != null || person.Role != lecturerUpdate.Role) ? person.Role : lecturerUpdate.Role;
                lecturerUpdate.Gender = (person.Gender != null || person.Gender != lecturerUpdate.Gender) ? person.Gender : lecturerUpdate.Gender;

                var personEntity = _mapper.Map<Lecturer>(person);

                result = await _personRepository.UpdatePerson(personEntity);

                _unitOfWork.SaveChanges();
            }
    
            return result;
        }

        public async Task<string> DeleteStudent<T>(T person) where T : PersonDTO
        {
            string result = string.Empty;

            if (typeof(T) == typeof(StudentDTO))
            {
                var personEntity = _mapper.Map<Student>(person);

                personEntity.IsDelete = true;

                result = await _personRepository.DeletePerson(personEntity);

                _unitOfWork.SaveChanges();
            }
            else if (typeof(T) == typeof(LecturerDTO))
            {
                var personEntity = _mapper.Map<Lecturer>(person);

                personEntity.IsDelete = true;

                result = await _personRepository.DeletePerson(personEntity);

                _unitOfWork.SaveChanges();
            } 
                
            return result;
        }
    }
}
