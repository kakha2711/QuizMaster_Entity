
using Microsoft.EntityFrameworkCore;
using QuizMaster.Domane.Enum;
using QuizMaster.Domane.Models.Entities;

namespace QuizMaster.InfroStructure.Data
{
    public class QuizMasterDbContext : DbContext
    {
        public QuizMasterDbContext(DbContextOptions<QuizMasterDbContext> options) : base(options)
        {
        }

      
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentTestResult> StudentTestResults { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestSubmissions> TestSubmissions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Test>()
                .Property(t => t.Title)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            modelBuilder.Entity<Test>()
                .HasIndex(t => t.Title)
                .IsUnique();
            modelBuilder.Entity<Test>()
                .Property(t => t.Topic)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            modelBuilder.Entity<Test>()
                .Property(t=>t.QuestionsNumber)
                .IsRequired()
                .HasColumnType("int")
                .HasDefaultValue(0);
            modelBuilder.Entity<Test>()
                .Property(t => t.MaximumScore)
                .IsRequired()
                .HasColumnType("float")
                .HasDefaultValue(0);
            modelBuilder.Entity<Test>()
                .Property(t => t.DurationMinutes)
                .IsRequired()
                .HasColumnType("float")
                .HasDefaultValue(0);
            modelBuilder.Entity<Test>()
                .Property(t => t.PassingPercentage)
                .IsRequired()
                .HasColumnType("int")
                .HasDefaultValue(0);
            modelBuilder.Entity<Test>()
                .Property(t => t.IsDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithOne(q => q.Test)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Test>()
                .HasMany(q => q.StudentTestResults)
                .WithOne(str => str.Test)
                .HasForeignKey(str => str.TestId)
                .OnDelete(DeleteBehavior.Cascade);





            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionText)
                .IsRequired()
                .HasColumnType("nvarchar(500)");
            modelBuilder.Entity<Question>()
                .Property(q => q.IsDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionTestId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.TestSubmissions)
                .WithOne(ts => ts.Question)
                .HasForeignKey(ts => ts.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Question>()
                .Property(q => q.ChoiceQuestion)
                .IsRequired()
                .HasColumnType("int");

           



            modelBuilder.Entity<Answer>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Answer>()
                .Property(a => a.AnswerText)
                .IsRequired()
                .HasColumnType("nvarchar(500)");
            modelBuilder.Entity<Answer>()
                .Property(a => a.IsCorrect)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<Answer>()
                .Property(a => a.isDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionTestId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Answer>()
                .HasMany(a => a.TestSubmissions)
                .WithOne(ts => ts.Answer)
                .HasForeignKey(ts => ts.AnswerId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<TestSubmissions>()
                .HasKey(ts => ts.Id);
            modelBuilder.Entity<TestSubmissions>()
                .HasOne(ts => ts.Student)
                .WithMany(s => s.TestSubmissions)
                .HasForeignKey(ts => ts.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TestSubmissions>()
                .HasOne(ts => ts.Question)
                .WithMany(q => q.TestSubmissions)
                .HasForeignKey(ts => ts.QuestionId);
            modelBuilder.Entity<TestSubmissions>()
                .HasOne(ts => ts.Answer)
                .WithMany(a => a.TestSubmissions)
                .HasForeignKey(ts => ts.AnswerId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TestSubmissions>()
                .Property(ts => ts.CorrectAnswer)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<TestSubmissions>()
                .Property(ts => ts.IsDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);

            modelBuilder.Entity<StudentTestResult>()
                .HasKey(str => str.Id);
            modelBuilder.Entity<StudentTestResult>()
                .Property(str => str.StartDate)
                .IsRequired()
                .HasColumnType("datetime");
            modelBuilder.Entity<StudentTestResult>()
                .Property(str => str.Score)
                .IsRequired()
                .HasColumnType("float")
                .HasDefaultValue(0);
            modelBuilder.Entity<StudentTestResult>()
                .Property(str => str.IsDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<StudentTestResult>()
                .HasOne(str => str.Student)
                .WithMany(s => s.StudentTestResults)
                .HasForeignKey(str => str.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StudentTestResult>()
                .HasOne(str => str.Test)
                .WithMany(t => t.StudentTestResults)
                .HasForeignKey(str => str.TestId)
                .OnDelete(DeleteBehavior.Cascade);
       


            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Student>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Student_FirsName",
                        "[FirsName] NOT LIKE '%[^a-zA-Z]%' or [FirsName] NOT LIKE N'%[^ა-ჰ]%'"))
                .Property(s=>s.FirsName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Student>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Student_Lastname",
                        "[Lastname] NOT LIKE '%[^a-zA-Z]%' or [Lastname] NOT LIKE N'%[^ა-ჰ]%'"))
                .Property(s => s.Lastname)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Student>()
                .ToTable(s=>s.HasCheckConstraint
                        ("CHK_Student_Email",
                        "[Email] NOT LIKE '%[^a-zA-Z0-9@._-]%'"))
                .Property(s => s.Email)
                .IsRequired()
                .HasColumnType("varchar(255)");
            modelBuilder.Entity<Student>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Student_PersonalNumber",
                        "[PersonalNumber] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'"))
                .Property(s => s.PersonalNumber)
                .IsRequired()
                .HasColumnType("varchar(11)");
            modelBuilder.Entity<Student>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Student_PhoneNumber",
                        "[PhoneNumber] LIKE '+995[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'"))
                .Property(s => s.PhoneNumber)
                .IsRequired()
                .HasColumnType("varchar(13)");
           modelBuilder.Entity<Student>()
                .Property(s => s.UserName)
                .IsRequired()
                .HasColumnType("varchar(15)");
            modelBuilder.Entity<Student>()
                .Property(s => s.Password)
                .IsRequired()
                .HasColumnType("varchar(100)");  // ვიკითხოთ, რომ პაროლი უნდა იყოს hashed, ამიტომ varchar(100) საკმარისი იქნება.
            modelBuilder.Entity<Student>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Student_VerificationCode",
                        "[VerificationCode] LIKE '[0-9][0-9][0-9][0-9]'"))
                .Property(s => s.VerificationCode)
                .IsRequired()
                .HasColumnType("varchar(4)");
            modelBuilder.Entity<Student>()
                .Property(s => s.IsVerified)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<Student>()
                .Property(s => s.IsDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);



            modelBuilder.Entity<Student>()
                .Property(s => s.Grade)
                .IsRequired()
                .HasColumnType("float")
                .HasDefaultValue(0);
            modelBuilder.Entity<Student>()
                .HasMany(s => s.StudentTestResults)
                .WithOne(str => str.Student)
                .HasForeignKey(str => str.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Student>()
                .HasMany(s => s.TestSubmissions)
                .WithOne(ts => ts.Student)
                .HasForeignKey(ts => ts.StudentId)
                .OnDelete(DeleteBehavior.Cascade);







            modelBuilder.Entity<Lecturer>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Lecturer>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Lecturer_FirsName",
                        "[FirsName] NOT LIKE '%[^A-Za-z]%' or [FirsName] NOT LIKE N'%[^ა-ჰ]%'"))
                .Property(s => s.FirsName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Lecturer>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Lecturer_Lastname",
                        "[Lastname] NOT LIKE '%[^A-Za-z]%' or [Lastname] NOT LIKE N'%[^ა-ჰ]%'"))
                .Property(s => s.Lastname)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Lecturer>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Lecturer_Email",
                        "[Email] NOT LIKE '%[^A-Za-z0-9@._-]%'"))
                .Property(s => s.Email)
                .IsRequired()
                .HasColumnType("varchar(255)");
            modelBuilder.Entity<Lecturer>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Lecturer_PersonalNumber",
                        "[PersonalNumber] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'"))
                .Property(s => s.PersonalNumber)
                .IsRequired()
                .HasColumnType("varchar(11)");
            modelBuilder.Entity<Lecturer>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Lecturer_PhoneNumber",
                        "[PhoneNumber] LIKE '+995[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'"))
                .Property(s => s.PhoneNumber)
                .IsRequired()
                .HasColumnType("varchar(13)");
            modelBuilder.Entity<Lecturer>()
                 .Property(s => s.UserName)
                 .IsRequired()
                 .HasColumnType("varchar(15)");
            modelBuilder.Entity<Lecturer>()
                .Property(s => s.Password)
                .IsRequired()
                .HasColumnType("varchar(100)");  // ვიკითხოთ, რომ პაროლი უნდა იყოს hashed, ამიტომ varchar(100) საკმარისი იქნება.
            modelBuilder.Entity<Lecturer>()
                .ToTable(s => s.HasCheckConstraint
                        ("CHK_Lecturer_VerificationCode",
                        "[VerificationCode] LIKE '[0-9][0-9][0-9][0-9]'"))
                .Property(s => s.VerificationCode)
                .IsRequired()
                .HasColumnType("varchar(4)");
            modelBuilder.Entity<Lecturer>()
                .Property(s => s.IsVerified)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<Lecturer>()
                .Property(s => s.IsDelete)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(false);


        }
    }
}
