using Microsoft.EntityFrameworkCore;
using Students.Repository;
using Students.Repository.Models;
using Students.Web.Result;
using Students.Web.Services.Common;
using Students.Web.Services.Students.Dtos;

namespace Students.Web.Services.Students
{
    public class StudentService(StudentsDbContext dbContext) : IStudentService
    {
        public async Task<Result<Student>> CreateStudent(CreateStudentDto studentDto)
        {
            //dbContext.Students.AddAsync(studentDto);
            throw new NotImplementedException();
        }

        public async Task<Result<Student>> GetStudentById(int studentId)
        {
            var student = await dbContext.Students.AsNoTracking().SingleOrDefaultAsync(x => x.StudentId == studentId);

            if (student == null) {
                return Result<Student>.Failure(StudentErrors.NotFound(studentId));
            }

            return Result<Student>.Success(student);
        }

        public async Task<Result<PagedList<Student>>> GetStudents(GetStudentsFilter filter)
        {
            var query = dbContext.Students.AsNoTracking();

            if (string.IsNullOrWhiteSpace(filter.SearchText) == false)
            {
                query = query.Where(x => EF.Functions.Like(x.FirstName, $"%{filter.SearchText}%")
                        || EF.Functions.Like(x.LastName, $"%{filter.SearchText}%"));
            }
            
            var totalCount = query.Count();
                
            var items = await query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var result = new PagedList<Student>(items, totalCount, filter.PageNumber, filter.PageSize);
            return Result<PagedList<Student>>.Success(result);
        }
    }
}
