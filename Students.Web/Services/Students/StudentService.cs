using Students.Repository;
using Students.Repository.Models;
using Students.Web.Result;

namespace Students.Web.Services.Students
{
    public class StudentService(StudentsDbContext dbContext) : IStudentService
    {
        public Result<Student> GetStudentById(int studentId)
        {
            var student = dbContext.Students.SingleOrDefault(x => x.StudentId == studentId);

            if (student == null) {
                return Result<Student>.Failure(StudentErrors.NotFound(studentId));
            }

            return Result<Student>.Success(student);
        }

        public Result<IEnumerable<Student>> GetStudents()
        {
            return Result<IEnumerable<Student>>.Success(dbContext.Students);
        }
    }
}
