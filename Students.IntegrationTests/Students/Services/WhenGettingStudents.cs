using Students.Repository;
using Students.Repository.Models;
using Students.Web.Services.Students;
using Students.Web.Services.Students.Dtos;

namespace Students.IntegrationTests.Students.Services
{
    [Collection("Database collection")]
    public class WhenGettingStudents : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
    {
        private IStudentService _studentService;
        private StudentsDbContext _db;
        private Student _student1;

        public WhenGettingStudents(IntegrationTestFactory factory) : base(factory)
        {
            _studentService = new StudentService(factory.Db);
            _db = factory.Db;


            _student1 = _db.Add(new Student
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

            _db.Add(new Student
            {
                FirstName = "Student 2",
                LastName = "TestLast",
                Gender = Gender.Male
            });

            _db.Add(new Student
            {
                FirstName = "Student 3",
                LastName = "S3 Name",
                Gender = Gender.Female
            });

            _db.SaveChanges();
        }

        [Fact]
        public async Task ShouldGetStudentDataCorrectly()
        {
            var filter = new GetStudentsFilter(1, 10);
            var result = await _studentService.GetStudents(filter);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var records = result.Value;
            Assert.Equal(3, records.TotalCount);
        }

        [Fact]
        public async Task ShouldReturnRequestedCountCorrectly()
        {
            var filter = new GetStudentsFilter(1, 1);
            var result = await _studentService.GetStudents(filter);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var records = result.Value;
            Assert.Equal(3, records.TotalCount);
            Assert.Equal(1, records.CurrentPage);
        }

        [Fact]
        public async Task ShouldReturnEmptyArrayWhenNotFound()
        {
            var filter = new GetStudentsFilter(1, 10, "non-existing-text");
            var result = await _studentService.GetStudents(filter);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value.Items);
        }
    }
}