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
            modelBuilder.Entity<Address>().ToTable("Addresses");
        }
    }
}
