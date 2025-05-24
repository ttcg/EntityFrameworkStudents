using Students.Repository.Models;
using Students.Web.Models;
using Students.Web.ViewModels;
using System.Net.Http.Json;

namespace Students.IntegrationTests.Students.Endpoints
{
    public class WhenCreatingStudent : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
    {
        public WhenCreatingStudent(IntegrationTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task ShouldAddStudent()
        {
            // Arrange
            var studentToAdd = new CreateStudentModel
            {
                FirstName = "Test",
                LastName = "TestLast",
                Gender = Gender.Male
            };
            var response = await _client.PostAsJsonAsync($"/Students", studentToAdd);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var student = await GetDtoFromResponse<StudentViewModel>(response);

            Assert.Equal(studentToAdd.FirstName, student.FirstName);
            Assert.Equal(studentToAdd.LastName, student.LastName);
            Assert.Equal(studentToAdd.Gender, student.Gender);
            Assert.True(student.StudentId > 0);
        }        
    }
}
