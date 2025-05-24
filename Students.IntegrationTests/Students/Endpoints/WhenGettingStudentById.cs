using Students.Repository.Models;
using Students.Web.ViewModels;

namespace Students.IntegrationTests.Students.Endpoints
{
    public class WhenGettingStudentById : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
    {
        private Student _student1;

        public WhenGettingStudentById(IntegrationTestFactory factory): base(factory)
        {
            _student1 = factory.Db.Add(new Student
            {
                FirstName = "Test",
                LastName = "TestLast",
                Gender = Gender.Male,
                Enrolments = [
                    new Enrolment()
                    {
                        Course = Course.Maths,
                        Status = EnrolmentStatus.Withdrawn
                    }
                ]
            }).Entity;
            factory.Db.SaveChanges();
        }

        [Fact]
        public async Task ShouldReturnStudent()
        {
            // Arrange
            var response = await _client.GetAsync($"/Students/{_student1.StudentId}");

            // Assert
            response.EnsureSuccessStatusCode();

            var student = await GetDtoFromResponse<StudentViewModel>(response);            

            Assert.Equal(_student1.FirstName, student.FirstName);
            Assert.Equal(_student1.LastName, student.LastName);
            Assert.Equal(_student1.Gender, student.Gender);
            Assert.Equal(_student1.StudentId, student.StudentId);
            Assert.Single(_student1.Enrolments);
        }        
    }
}
