using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizMaster.InfroStructure.Migrations
{
    /// <inheritdoc />
    public partial class QuizMasterDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirsName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(13)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "varchar(11)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(15)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    VerificationCode = table.Column<string>(type: "varchar(4)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                    table.CheckConstraint("CHK_Lecturer_Email", "[Email] NOT LIKE '%[^A-Za-z0-9@._-]%'");
                    table.CheckConstraint("CHK_Lecturer_FirsName", "[FirsName] NOT LIKE '%[^A-Za-z]%' or [FirsName] NOT LIKE N'%[^ა-ჰ]%'");
                    table.CheckConstraint("CHK_Lecturer_Lastname", "[Lastname] NOT LIKE '%[^A-Za-z]%' or [Lastname] NOT LIKE N'%[^ა-ჰ]%'");
                    table.CheckConstraint("CHK_Lecturer_PersonalNumber", "[PersonalNumber] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                    table.CheckConstraint("CHK_Lecturer_PhoneNumber", "[PhoneNumber] LIKE '+995[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                    table.CheckConstraint("CHK_Lecturer_VerificationCode", "[VerificationCode] LIKE '[0-9][0-9][0-9][0-9]'");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    FirsName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(13)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "varchar(11)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(15)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    VerificationCode = table.Column<string>(type: "varchar(4)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.CheckConstraint("CHK_Student_Email", "[Email] NOT LIKE '%[^a-zA-Z0-9@._-]%'");
                    table.CheckConstraint("CHK_Student_FirsName", "[FirsName] NOT LIKE '%[^a-zA-Z]%' or [FirsName] NOT LIKE N'%[^ა-ჰ]%'");
                    table.CheckConstraint("CHK_Student_Lastname", "[Lastname] NOT LIKE '%[^a-zA-Z]%' or [Lastname] NOT LIKE N'%[^ა-ჰ]%'");
                    table.CheckConstraint("CHK_Student_PersonalNumber", "[PersonalNumber] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                    table.CheckConstraint("CHK_Student_PhoneNumber", "[PhoneNumber] LIKE '+995[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                    table.CheckConstraint("CHK_Student_VerificationCode", "[VerificationCode] LIKE '[0-9][0-9][0-9][0-9]'");
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    QuestionsNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MaximumScore = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    DurationMinutes = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    PassingPercentage = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ChoiceQuestion = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    CompletedOngoingTests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTestResults_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTestResults_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    QuestionTestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionTestId",
                        column: x => x.QuestionTestId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSubmissions_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestSubmissions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestSubmissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionTestId",
                table: "Answers",
                column: "QuestionTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestResults_StudentId",
                table: "StudentTestResults",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestResults_TestId",
                table: "StudentTestResults",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Title",
                table: "Tests",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissions_AnswerId",
                table: "TestSubmissions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissions_QuestionId",
                table: "TestSubmissions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissions_StudentId",
                table: "TestSubmissions",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "StudentTestResults");

            migrationBuilder.DropTable(
                name: "TestSubmissions");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
