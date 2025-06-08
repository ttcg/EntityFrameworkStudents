using Students.Repository.Models;
using Students.Web.Services.Students;

namespace Students.IntegrationTests.Students.Services
{
    [Collection(TestCollections.SqlIntegration)]
    public class WhenDeletingStudent : BaseIntegrationTest
    {
        private IStudentService _studentService;
        private Student _student;

        public WhenDeletingStudent(IntegrationTestFactory factory) : base(factory)
        {
            _studentService = new StudentService(factory.Db);

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
        public async Task ShouldDeleteStudentData()
        {
            var result = await _studentService.DeleteStudent(_student.StudentId);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var student = await _db.Students.FindAsync(_student.StudentId);
            Assert.Null(student);
        }

        [Fact]
        public async Task ShouldThrowNotFoundError()
        {
            var result = await _studentService.DeleteStudent(999);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.NotFound, result.Error.Code);
        }
    }
}