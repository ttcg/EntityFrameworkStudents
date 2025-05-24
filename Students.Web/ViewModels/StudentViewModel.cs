using Students.Repository.Models;
using System.Text.Json.Serialization;

namespace Students.Web.ViewModels
{
    public class StudentViewModel
    {        
        public StudentViewModel()
        {

        }

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

        public int StudentId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; init; }

        public DateTime DateModified { get; init; }
        public DateTime DateCreated { get; init; }

        public List<EnrolmentViewModel> Enrolments { get; init; } = [];

        public class EnrolmentViewModel 
        {
            public EnrolmentViewModel()
            {
                
            }
            public EnrolmentViewModel(Enrolment enrolment)
            {
                Course = enrolment.Course;
                Status = enrolment.Status;
                EnrolledDate = enrolment.DateCreated;
            }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Course Course { get; init; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public EnrolmentStatus Status { get; init; }
            public DateTime EnrolledDate { get; init; }
        }
    }
}
