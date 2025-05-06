using Students.Repository;
using Students.Repository.Models;
using Students.Web.Services.Students;
using Students.Web.Services.Students.Dtos;

namespace Students.IntegrationTests.Students.Services
{
    public class WhenUpdatingStudent : IClassFixture<IntegrationTestFactory>
    {
        private IStudentService _studentService;
        private StudentsDbContext _db;
        private Student _student;

        public WhenUpdatingStudent(IntegrationTestFactory factory)
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
        public async Task ShouldUpdateStudentData()
        {
            var dto = new UpdateStudentDto
            {
                Id = _student.StudentId,
                FirstName = "Updated First",
                LastName = "Updated Last",
                Gender = Gender.Female
            };

            var result = await _studentService.UpdateStudent(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var updatedStudent = result.Value;
            Assert.Equal(updatedStudent.FirstName, dto.FirstName);
            Assert.Equal(updatedStudent.LastName, dto.LastName);
            Assert.True(updatedStudent.Addresses.Any());
        }

        [Fact]
        public async Task ShouldThrowNotFoundError()
        {
            var dto = new UpdateStudentDto
            {
                Id = 999,
                FirstName = "Updated First",
                LastName = "Updated Last",
                Gender = Gender.Female
            };
            var result = await _studentService.UpdateStudent(dto);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.NotFound, result.Error.Code);
        }
    }
}