using Students.Repository.Models;

namespace Students.Web.Services.Students.Dtos
{
    public class CreateStudentDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Gender Gender { get; set; }
        public List<AddressDto> Addresses { get; set; } = [];

        public class AddressDto
        {
            public string City { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public bool IsCurrent { get; set; }
        }
    }
}
