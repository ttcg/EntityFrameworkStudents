using Students.Repository.Models;

namespace Students.Web.Services.Students.Dtos
{
    public class CreateStudentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
