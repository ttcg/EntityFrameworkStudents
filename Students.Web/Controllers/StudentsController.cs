using Microsoft.AspNetCore.Mvc;
using Students.Repository;
using Students.Repository.Models;

namespace Students.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<StudentsController> _logger;
        private readonly StudentsDbContext dbContext;

        public StudentsController(ILogger<StudentsController> logger, StudentsDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return dbContext.Students.ToList();
        }
    }
}
