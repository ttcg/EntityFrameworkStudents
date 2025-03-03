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
    }
}
