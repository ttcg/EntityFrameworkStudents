using Students.Web.Result;

namespace Students.Web.Services.Teachers
{
    public class TeacherErrors
    {
        public static Error NotFound(int teacherId) => new Error(
        "Teachers.NotFound", $"TeacherId: {teacherId} not found");
    }
}
