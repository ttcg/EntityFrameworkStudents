using Students.Repository;
using Students.Repository.Models;
using Students.Web.Services.Students;

namespace Students.IntegrationTests.Students.Services
{
    public class WhenCompletingCourse : IClassFixture<IntegrationTestFactory>
    {
        private IStudentService _studentService;
        private StudentsDbContext _db;
        private Student _student1;

        public WhenCompletingCourse(IntegrationTestFactory factory)
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
                ],
                Enrolments = [
                    new Enrolment()
                    {
                        Course = Course.Physics,
                        Status = EnrolmentStatus.InProgress
                    },
                    new Enrolment()
                    {
                        Course = Course.Maths,
                        Status = EnrolmentStatus.Completed
                    },
                    new Enrolment()
                    {
                        Course = Course.Chemistry,
                        Status = EnrolmentStatus.Withdrawn
                    }
                ]
            }).Entity;
            _db.SaveChanges();
        }

        [Fact]
        public async Task ShouldCompleteInProgressCourse()
        {
            var result = await _studentService.CompleteCourse(_student1.StudentId, Course.Physics);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var enrolment = result.Value;
            Assert.Equal(Course.Physics, enrolment.Course);
            Assert.Equal(_student1.StudentId, enrolment.StudentId);
            Assert.Equal(EnrolmentStatus.Completed, enrolment.Status);
        }

        [Fact]
        public async Task ShouldNotComplete_AlreadyCompletedCourse()
        {
            var result = await _studentService.CompleteCourse(_student1.StudentId, Course.Maths);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.EnrolmentNotFound, result.Error.Code);
        }

        [Fact]
        public async Task ShouldNotComplete_WithdrawnCourse()
        {
            var result = await _studentService.CompleteCourse(_student1.StudentId, Course.Chemistry);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.EnrolmentNotFound, result.Error.Code);
        }

        [Fact]
        public async Task ShouldNotCompleteWithoutEnrolment()
        {
            var result = await _studentService.CompleteCourse(_student1.StudentId, Course.Bio);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.EnrolmentNotFound, result.Error.Code);
        }

        [Fact]
        public async Task ShouldThrowNotFoundErrorForWrongStudent()
        {
            var result = await _studentService.CompleteCourse(999, Course.Maths);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.NotFound, result.Error.Code);
        }
    }
}