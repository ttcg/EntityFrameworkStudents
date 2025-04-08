using Microsoft.EntityFrameworkCore;
using Students.Repository;
using Students.Repository.Models;
using Students.Web.Result;
using Students.Web.Services.Common;
using Students.Web.Services.Students.Dtos;

namespace Students.Web.Services.Students
{
    public class StudentService(StudentsDbContext db) : IStudentService
    {
        public async Task<Result<Student>> CreateStudent(CreateStudentDto studentDto)
        {
            var student = await db.AddAsync(new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Gender = studentDto.Gender
            });

            await db.SaveChangesAsync();

            return Result<Student>.Success(student.Entity);
        }

        public async Task<Result<bool>> DeleteStudent(int studentId)
        {
            var student = await db.Students.FindAsync(studentId);

            if (student == null)
            {
                return Result<bool>.Failure(StudentErrors.NotFound(studentId));
            }

            db.Remove(student);

            await db.SaveChangesAsync();
            
            return Result<bool>.Success(true);
        }

        public async Task<Result<Student>> GetStudentById(int studentId)
        {
            var student = await db.Students.AsNoTracking()
                .Include(x => x.Enrolments)
                .SingleOrDefaultAsync(x => x.StudentId == studentId);

            if (student == null)
            {
                return Result<Student>.Failure(StudentErrors.NotFound(studentId));
            }

            return Result<Student>.Success(student);
        }

        public async Task<Result<PagedList<Student>>> GetStudents(GetStudentsFilter filter)
        {
            var query = db.Students.AsNoTracking();

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

        public async Task<Result<Student>> UpdateStudent(UpdateStudentDto studentDto)
        {
            var student = await db.Students.FindAsync(studentDto.Id);

            if (student == null)
            {
                return Result<Student>.Failure(StudentErrors.NotFound(studentDto.Id));
            }

            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Gender = studentDto.Gender;

            db.Entry(student).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Result<Student>.Success(student);
        }
    }
}
