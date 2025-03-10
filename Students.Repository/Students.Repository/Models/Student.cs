using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Repository.Models
{
    public class Student : IHaveAuditData
    {
        public int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Gender Gender { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
