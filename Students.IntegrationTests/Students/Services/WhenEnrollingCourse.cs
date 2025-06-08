using Students.Repository.Models;
using Students.Web.Services.Students;

namespace Students.IntegrationTests.Students.Services
{
    [Collection(TestCollections.SqlIntegration)]
    public class WhenEnrollingStudent : BaseIntegrationTest
    {
        private IStudentService _studentService;
        private Student _student1;

        public WhenEnrollingStudent(IntegrationTestFactory factory) : base(factory)
        {
            _studentService = new StudentService(factory.Db);

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
        public async Task ShouldEnrolCourseForWithdrawn()
        {
            var result = await _studentService.EnrolCourse(_student1.StudentId, Course.Chemistry);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var enrolment = result.Value;
            Assert.Equal(Course.Chemistry, enrolment.Course);
            Assert.Equal(_student1.StudentId, enrolment.StudentId);
            Assert.Equal(EnrolmentStatus.InProgress, enrolment.Status);
        }

        [Fact]
        public async Task ShouldEnrolNewCourse()
        {
            var result = await _studentService.EnrolCourse(_student1.StudentId, Course.Bio);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            var enrolment = result.Value;
            Assert.Equal(Course.Bio, enrolment.Course);
            Assert.Equal(_student1.StudentId, enrolment.StudentId);
            Assert.Equal(EnrolmentStatus.InProgress, enrolment.Status);
        }

        [Fact]
        public async Task ShouldNotEnrolCompletedCourse()
        {
            var result = await _studentService.EnrolCourse(_student1.StudentId, Course.Maths);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.EnrolmentAlreadyExist, result.Error.Code);
        }

        [Fact]
        public async Task ShouldNotEnrolInProgressCourse()
        {
            var result = await _studentService.EnrolCourse(_student1.StudentId, Course.Physics);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.EnrolmentAlreadyExist, result.Error.Code);
        }

        [Fact]
        public async Task ShouldThrowNotFoundErrorForWrongStudent()
        {
            var result = await _studentService.EnrolCourse(999, Course.Maths);
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(StudentErrors.Constants.NotFound, result.Error.Code);
        }
    }
}