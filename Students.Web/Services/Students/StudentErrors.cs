using Students.Web.Result;

namespace Students.Web.Services.Students
{
    public class StudentErrors
    {
        public static Error NotFound(int studentId) => new Error(
        "Students.NotFound", $"StudentId: {studentId} not found");
    }
}
