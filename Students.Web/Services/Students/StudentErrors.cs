using Students.Repository.Models;
using Students.Web.Result;

namespace Students.Web.Services.Students
{
    public class StudentErrors
    {
        public static Error NotFound(int studentId) => new Error(
        $"Students.{nameof(NotFound)}", $"StudentId: {studentId} not found");

        public static Error EnrolmentAlreadyExist(int studentId, Course course) => new Error(
        $"Students.{nameof(EnrolmentAlreadyExist)}", $"StudentId: {studentId} has already enrolled on a course: {course}");

        public static Error EnrolmentNotFound(int studentId, Course course) => new Error(
        $"Students.{nameof(EnrolmentNotFound)}", $"StudentId: {studentId} does not have an enrolment in progress for a course: {course}");
    }
}
