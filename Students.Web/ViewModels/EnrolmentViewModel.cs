using Students.Repository.Models;

namespace Students.Web.ViewModels
{
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
