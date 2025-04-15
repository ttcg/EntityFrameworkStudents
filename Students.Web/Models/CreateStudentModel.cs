using Students.Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace Students.Web.Models
{
    public class CreateStudentModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public List<AddressModel> AddressList { get; set; } = [];

        public class AddressModel
        {
            public string City { get; set; }
            public string Line1 { get; set; }
            public string? Line2 { get; set; }
            public bool IsCurrent { get; set; }
        }
    }
}
