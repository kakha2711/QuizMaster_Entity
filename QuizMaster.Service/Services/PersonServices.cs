
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
            var personEntity = _mapper.Map<Person>(person);

            var result = await _personRepository.UpdatePerson(personEntity);

            _unitOfWork.SaveChanges();
            return result;
        }
        public async Task<string> DeleteStudent<T>(T person) where T : PersonDTO
        {
            var personEntity = _mapper.Map<Person>(person);

            var result = await _personRepository.DeletePerson(personEntity);

            _unitOfWork.SaveChanges();
            return result;
        }
    }
}
