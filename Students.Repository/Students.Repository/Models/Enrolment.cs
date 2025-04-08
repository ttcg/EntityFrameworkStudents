namespace Students.Repository.Models
{
    public class Enrolment : IHaveAuditData
    {
        public int EnrolmentId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Course Course { get; set; }
        public EnrolmentStatus Status { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public enum Course
    {
        Chemistry,
        Maths,
        Physics,
        Bio
    }

    public enum EnrolmentStatus
    {
        InProgress,
        Withdrawn,
        Completed
    }
}
