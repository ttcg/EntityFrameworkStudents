using Students.Repository.Models;
using Students.Web.Result;
using Students.Web.Services.Common;
using Students.Web.Services.Students.Dtos;

namespace Students.Web.Services.Students
{
    public interface IStudentService
    {
        Task<Result<Student>> GetStudentById(int studentId);

        Task<Result<PagedList<Student>>> GetStudents(GetStudentsFilter filter);

        Task<Result<Student>> CreateStudent(CreateStudentDto studentDto);
    }
}
