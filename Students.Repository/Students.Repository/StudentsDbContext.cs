using Microsoft.EntityFrameworkCore;
using Students.Repository.Models;

namespace Students.Repository
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
            : base(options)
        {
            SavingChanges += SavingChangesHandler;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }   

        private void SavingChangesHandler(object? sender, SavingChangesEventArgs e)
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is IHaveAuditData))
            {
                var auditedEntity = (IHaveAuditData)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        {
                            auditedEntity.DateModified = DateTime.UtcNow;
                            break;
                        }
                    case EntityState.Added:
                        {
                            auditedEntity.DateModified = DateTime.UtcNow;
                            auditedEntity.DateCreated = auditedEntity.DateModified;
                            break;
                        }
                }

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SeedStudents(modelBuilder);
            //SeedTeachers(modelBuilder);
            //SeedEnrolments(modelBuilder);

            modelBuilder.Entity<Address>().ToTable("Addresses");
        }

        private static void SeedStudents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                            new Student
                            {
                                StudentId = 1,
                                FirstName = "John",
                                LastName = "Smith",
                                Gender = Gender.Male
                            },
                            new Student
                            {
                                StudentId = 2,
                                FirstName = "Aaron",
                                LastName = "Hanwell",
                                Gender = Gender.Male
                            },
                            new Student { StudentId = 3, FirstName = "Quest", LastName = "Ball", Gender = Gender.Male },
                            new Student { StudentId = 4, FirstName = "Caroline", LastName = "Turner", Gender = Gender.Female },
                            new Student { StudentId = 5, FirstName = "David", LastName = "Smith", Gender = Gender.Male },
                            new Student { StudentId = 6, FirstName = "Clorie", LastName = "Hanwell", Gender = Gender.Female },
                            new Student { StudentId = 7, FirstName = "Pecca", LastName = "Owell", Gender = Gender.Other },
                            new Student { StudentId = 8, FirstName = "Bad", LastName = "Apple", Gender = Gender.Female }
                            );
        }

        private static void SeedTeachers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherId = 1, FirstName = "Micah", LastName = "Davies" },
                new Teacher { TeacherId = 2, FirstName = "Ben", LastName = "Foster" },
                new Teacher { TeacherId = 3, FirstName = "Cameron", LastName = "Mason" },
                new Teacher { TeacherId = 4, FirstName = "Zac", LastName = "Cooke" },
                new Teacher { TeacherId = 5, FirstName = "Robbie", LastName = "Fox" }
                );
        }

        private static void SeedEnrolments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrolment>().HasData(
                new Enrolment
                {
                    Course = Course.Bio,
                    EnrolmentId = 1,
                    Status = EnrolmentStatus.InProgress,
                    StudentId = 1
                },
                new Enrolment
                {
                    Course = Course.Maths,
                    EnrolmentId = 2,
                    Status = EnrolmentStatus.InProgress,
                    StudentId = 1
                },
                new Enrolment
                {
                    Course = Course.Maths,
                    EnrolmentId = 3,
                    Status = EnrolmentStatus.Withdrawn,
                    StudentId = 2
                },
                new Enrolment
                {
                    Course = Course.Chemistry,
                    EnrolmentId = 4,
                    Status = EnrolmentStatus.InProgress,
                    StudentId = 2
                }
            );
        }
    }
}
