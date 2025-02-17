using Students.Repository;
using Students.Repository.Models;
using Students.Web.Result;

namespace Students.Web.Services.Teachers
{
    public class TeacherService(StudentsDbContext dbContext) : ITeacherService
    {
        public Result<Teacher> GetTeacherById(int teacherId)
        {
            var teacher = dbContext.Teachers.SingleOrDefault(x => x.TeacherId == teacherId);

            if (teacher == null)
            {
                return Result<Teacher>.Failure(TeacherErrors.NotFound(teacherId));
            }

            return Result<Teacher>.Success(teacher);
        }

        public Result<IEnumerable<Teacher>> GetTeachers()
        {
            return Result<IEnumerable<Teacher>>.Success(dbContext.Teachers);
        }
    }
}
