using Microsoft.EntityFrameworkCore;
using Students.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Repository
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public string DbPath { get; }

        public StudentsDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "students.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, FirstName = "John", LastName = "Smith", Gender = Gender.Male },
                new Student { StudentId = 2, FirstName = "Aaron", LastName = "Hanwell", Gender = Gender.Male },
                new Student { StudentId = 3, FirstName = "Quest", LastName = "Ball", Gender = Gender.Male },
                new Student { StudentId = 4, FirstName = "Caroline", LastName = "Turner", Gender = Gender.Female },
                new Student { StudentId = 5, FirstName = "David", LastName = "Smith", Gender = Gender.Male },
                new Student { StudentId = 6, FirstName = "Clorie", LastName = "Hanwell", Gender = Gender.Female },
                new Student { StudentId = 7, FirstName = "Pecca", LastName = "Owell", Gender = Gender.Other },
                new Student { StudentId = 8, FirstName = "Bad", LastName = "Apple", Gender = Gender.Female }
                );
        }
    }
}
