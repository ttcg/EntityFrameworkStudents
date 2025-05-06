using Microsoft.AspNetCore.Http.HttpResults;
using Students.Repository.Models;
using Students.Web.Result;

namespace Students.Web.Services.Students
{
    public class StudentErrors
    {
        public class Constants
        {
            public const string NotFound = "Students.NotFound";
            public const string EnrolmentAlreadyExist = "Students.EnrolmentAlreadyExist";
            public const string EnrolmentNotFound = "Students.EnrolmentNotFound";
        }

        public static Error NotFound(int studentId) => new Error(
        Constants.NotFound, $"StudentId: {studentId} not found");

        public static Error EnrolmentAlreadyExist(int studentId, Course course) => new Error(
        Constants.EnrolmentAlreadyExist, $"StudentId: {studentId} has already enrolled on a course: {course}");

        public static Error EnrolmentNotFound(int studentId, Course course) => new Error(
        Constants.EnrolmentNotFound, $"StudentId: {studentId} does not have an enrolment in progress for a course: {course}");
    }

    
}
