using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Students.Repository;
using Students.Repository.Models;
using Students.Web.Services.Students;
using Students.Web.Services.Teachers;

namespace Students.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService) : ControllerBase
    {       
        private readonly ILogger<TeachersController> _logger = logger;

        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            var result = teacherService.GetTeachers();

            if (result.IsSuccess)
            {
                return result.Value;
            }

            return Enumerable.Empty<Teacher>();
        }

        [HttpGet("{teacherId}")]
        public IActionResult GetById([FromRoute] int teacherId)
        {
            var result = teacherService.GetTeacherById(teacherId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }
    }
}
