using Students.Repository.Models;
using Students.Web.Result;

namespace Students.Web.Services.Students
{
    public interface IStudentService
    {
        Result<Student> GetStudentById(int studentId);

        Result<IEnumerable<Student>> GetStudents();
    }
}
