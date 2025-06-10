using Microsoft.EntityFrameworkCore;
using Students.Repository;
using Students.Repository.Models;

namespace Students.Web.DataSeeding
{
    public static class TestData
    {
        public static async Task SeedAsync(WebApplication app, IConfiguration config)
        {
            var seedTestData = config.GetValue<bool>("SeedTestData", false);

            if (seedTestData)
            {
                try
                {
                    using var scope = app.Services.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();

                    await InsertTestData(context);
                }
                catch (Exception ex)
                {
                    // Log errors or throw
                    Console.WriteLine($"Seeding failed: {ex.Message}");
                    throw;
                }
            }
        }

        private static async Task InsertTestData(StudentsDbContext context)
        {
            await InsertStudentData(context);
            await InsertTeacherData(context);
            await InsertEnrolmentData(context);
        }

        private static async Task InsertEnrolmentData(StudentsDbContext context)
        {
            if (!context.Enrolments.Any())
            {
                try
                {
                    using var transaction = await context.Database.BeginTransactionAsync();
                    await context.Database.ExecuteSqlRawAsync(@"SET IDENTITY_INSERT [Enrolments] ON");

                    await context.Enrolments.AddRangeAsync(
                        [
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
                        ]);

                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync(@"SET IDENTITY_INSERT [Enrolments] OFF");
                    await transaction.CommitAsync();
                }
                finally
                {
                }
            }
        }

        private static async Task InsertTeacherData(StudentsDbContext context)
        {
            if (!context.Teachers.Any())
            {
                try
                {
                    using var transaction = await context.Database.BeginTransactionAsync();
                    await context.Database.ExecuteSqlRawAsync(@"SET IDENTITY_INSERT [Teachers] ON");

                    await context.Teachers.AddRangeAsync(
                        [
                            new Teacher { TeacherId = 1, FirstName = "Micah", LastName = "Davies" },
                        new Teacher { TeacherId = 2, FirstName = "Ben", LastName = "Foster" },
                        new Teacher { TeacherId = 3, FirstName = "Cameron", LastName = "Mason" },
                        new Teacher { TeacherId = 4, FirstName = "Zac", LastName = "Cooke" },
                        new Teacher { TeacherId = 5, FirstName = "Robbie", LastName = "Fox" }
                        ]);

                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync(@"SET IDENTITY_INSERT [Teachers] OFF");
                    await transaction.CommitAsync();
                }
                finally
                {
                }
            }
        }

        private static async Task InsertStudentData(StudentsDbContext context)
        {
            if (!context.Students.Any())
            {
                try
                {
                    using var transaction = await context.Database.BeginTransactionAsync();
                    await context.Database.ExecuteSqlRawAsync(@"SET IDENTITY_INSERT [Students] ON");

                    await context.Students.AddRangeAsync(
                        [
                            new Student
                        {
                            StudentId = 1,
                            FirstName = "John",
                            LastName = "Smith",
                            Gender = Gender.Male,
                            Addresses = [
                                new Address {
                                    Line1 = "123",
                                    Line2 = "My Street",
                                    IsCurrent = true,
                                    City = "London",
                                    Country = "UK",
                                },
                                new Address {
                                    Line1 = "Past address",
                                    IsCurrent = false,
                                    City = "Birmingham",
                                    Country = "UK",
                                }
                            ]
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
                        ]);

                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync(@"SET IDENTITY_INSERT [Students] OFF");
                    await transaction.CommitAsync();
                }
                finally
                {

                }
            }
        }
    }
}
