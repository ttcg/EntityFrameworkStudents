using Students.Repository.Models;
using Students.Web.Result;

namespace Students.Web.Services.Teachers
{
    public interface ITeacherService
    {
        Result<Teacher> GetTeacherById(int teacherId);

        Result<IEnumerable<Teacher>> GetTeachers();
    }
}
