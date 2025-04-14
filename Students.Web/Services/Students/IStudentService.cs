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

        Task<Result<Student>> UpdateStudent(UpdateStudentDto studentDto);

        Task<Result<bool>> DeleteStudent(int studentId);

        Task<Result<Enrolment>> EnrolCourse(int studentId, Course course);

        Task<Result<Enrolment>> CompleteCourse(int studentId, Course course);

        Task<Result<Enrolment>> WithdrawCourse(int studentId, Course course);
    }
}
