using Students.Web.Services.Students;
using Students.Web.Services.Students.Dtos;

namespace Students.IntegrationTests.Students.Services
{
    [Collection(TestCollections.SqlIntegration)]
    public class WhenCreatingStudent : BaseIntegrationTest
    {
        private IStudentService _studentService;

        public WhenCreatingStudent(IntegrationTestFactory factory): base(factory)
        {
            _studentService = new StudentService(factory.Db);
        }

        [Fact]
        public async Task ShouldCreateWithFullData()
        {
            var dto = new CreateStudentDto
            {
                FirstName = "Test",
                LastName = "TestLast",
                Gender = Repository.Models.Gender.Male,
                Addresses = [
                    new CreateStudentDto.AddressDto
                    {
                        Line1 = "Mars",
                        City = "London",
                        Country = "GB",
                        IsCurrent = true
                    }
                    ]
            };
            var result = await _studentService.CreateStudent(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var student = result.Value;
            Assert.Equal(student.FirstName, dto.FirstName);
            Assert.Equal(student.LastName, dto.LastName);
            Assert.True(student.Addresses.Any());
        }

        [Fact]
        public async Task ShouldCreateWithMinimalData()
        {
            var dto = new CreateStudentDto
            {
                FirstName = "Test",
                LastName = "TestLast",
                Gender = Repository.Models.Gender.Male
            };
            var result = await _studentService.CreateStudent(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var student = result.Value;
            Assert.Equal(student.FirstName, dto.FirstName);
            Assert.Equal(student.LastName, dto.LastName);
        }
    }
}