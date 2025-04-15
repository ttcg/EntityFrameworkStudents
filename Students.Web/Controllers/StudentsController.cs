using Microsoft.AspNetCore.Mvc;
using Students.Repository.Models;
using Students.Web.Models;
using Students.Web.Services.Common;
using Students.Web.Services.Students;
using Students.Web.Services.Students.Dtos;
using Students.Web.ViewModels;

namespace Students.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController(ILogger<StudentsController> logger, IStudentService studentService) : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Student>))]
        public async Task<IActionResult> Get([FromQuery] GetStudentsModel model)
        {
            var result = await studentService.GetStudents(new GetStudentsFilter(model.PageNumber, model.PageSize, model.SearchText));

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Ok(new PagedList<Student>(Enumerable.Empty<Student>(), 0, 0, 0));
        }

        [HttpGet("{studentId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentViewModel))]
        public async Task<IActionResult> GetById([FromRoute] int studentId)
        {
            var result = await studentService.GetStudentById(studentId);

            if (result.IsSuccess)
            {
                return Ok(new StudentViewModel(result.Value));
            }

            return NotFound(result.Error);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Student))]
        public async Task<IActionResult> Create([FromBody] CreateStudentModel model)
        {
            var result = await studentService.CreateStudent(
                new CreateStudentDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Addresses = model.AddressList?.Select(x => new CreateStudentDto.AddressDto
                    {
                        City = x.City,
                        IsCurrent = x.IsCurrent,
                        Line1 = x.Line1,
                        Line2 = x.Line2
                    }).ToList() ?? []
                });

            if (result.IsSuccess)
            {
                return CreatedAtAction(actionName: nameof(GetById), routeValues: new { studentId = result.Value.StudentId }, result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPut("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        public async Task<IActionResult> Update([FromRoute] int studentId, [FromBody] CreateStudentModel model)
        {
            var result = await studentService.UpdateStudent(
                new UpdateStudentDto
                {
                    Id = studentId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender
                });

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpDelete("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int studentId)
        {
            var result = await studentService.DeleteStudent(studentId);

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [HttpPost("{studentId}/courses/{course}/enrol")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrolmentViewModel))]
        public async Task<IActionResult> Enrol([FromRoute] int studentId, [FromRoute] Course course)
        {
            var result = await studentService.EnrolCourse(studentId, course);

            if (result.IsSuccess)
            {
                return Ok(new EnrolmentViewModel(result.Value));
            }

            return BadRequest(result.Error);
        }

        [HttpPost("{studentId}/courses/{course}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrolmentViewModel))]
        public async Task<IActionResult> CompleteEnrolment([FromRoute] int studentId, [FromRoute] Course course)
        {
            var result = await studentService.CompleteCourse(studentId, course);

            if (result.IsSuccess)
            {
                return Ok(new EnrolmentViewModel(result.Value));
            }

            return BadRequest(result.Error);
        }

        [HttpPost("{studentId}/courses/{course}/withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrolmentViewModel))]
        public async Task<IActionResult> WithdrawEnrolment([FromRoute] int studentId, [FromRoute] Course course)
        {
            var result = await studentService.WithdrawCourse(studentId, course);

            if (result.IsSuccess)
            {
                return Ok(new EnrolmentViewModel(result.Value));
            }

            return BadRequest(result.Error);
        }
    }
}
