using Students.Repository;
using Students.Repository.Models;
using Students.Web.Services.Students;

namespace Students.IntegrationTests.Students.Services
{
    public class WhenGettingStudentById : IClassFixture<IntegrationTestFactory>
    {
        private IStudentService _studentService;
        private StudentsDbContext _db;
        private Student _student;

        public WhenGettingStudentById(IntegrationTestFactory factory)
        {
            _studentService = new StudentService(factory.Db);
            _db = factory.Db;

            _student = _db.Add(new Student
            {
                FirstName = "Test",
                LastName = "TestLast",
                Gender = Gender.Male,
                Addresses = [
                    new Address()
                    {
                        Line1 = "Mars",
                        City = "London",
                        Country = "GB",
                        IsCurrent = true
                    }
                ]
            }).Entity;
            _db.SaveChanges();
        }

        [Fact]
        public async Task ShouldGetStudentData()
        {
            var result = await _studentService.GetStudentById(_student.StudentId);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var student = result.Value;
            Assert.Equal(student.FirstName, _student.FirstName);
            Assert.Equal(student.LastName, _student.LastName);
            Assert.Equal(student.Gender, _student.Gender);
            Assert.Equal(student.StudentId, _student.StudentId);
            Assert.True(student.Addresses.Any());
        }

        [Fact]
        public async Task ShouldThrowNotFoundError()
        {
            var result = await _studentService.GetStudentById(999);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.NotFound, result.Error.Code);
        }
    }
}