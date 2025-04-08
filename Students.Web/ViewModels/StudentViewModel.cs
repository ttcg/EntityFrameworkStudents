using Students.Repository.Models;

namespace Students.Web.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel(Student student)
        {
            StudentId = student.StudentId;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Gender = student.Gender;
            DateModified = student.DateModified;
            DateCreated = student.DateCreated;
            Enrolments = student.Enrolments?.Select(x => new EnrolmentViewModel(x)).ToList() ?? [];
        }

        public int StudentId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Gender Gender { get; set; }

        public DateTime DateModified { get; }
        public DateTime DateCreated { get; }

        public List<EnrolmentViewModel> Enrolments { get; } = [];

        public class EnrolmentViewModel 
        {
            public EnrolmentViewModel(Enrolment enrolment)
            {
                Course = enrolment.Course;
                Status = enrolment.Status;
                EnrolledDate = enrolment.DateCreated;
            }

            public Course Course { get; }
            public EnrolmentStatus Status { get; }
            public DateTime EnrolledDate { get; }
        }
    }
}
