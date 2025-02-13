namespace Students.Repository.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }
}
