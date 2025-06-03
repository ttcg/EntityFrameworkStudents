using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Students.Repository;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Students.IntegrationTests
{
    public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _container = new MsSqlBuilder().Build();

        public StudentsDbContext Db { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var connectionString = _container.GetConnectionString();
                services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<StudentsDbContext>) == service.ServiceType));
                services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
                services.AddDbContext<StudentsDbContext>((_, option) => option.UseSqlServer(connectionString));                
            });

            builder.UseEnvironment("Development");

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            var dbContext = Services.CreateScope().ServiceProvider.GetRequiredService<StudentsDbContext>();

            await dbContext.Database.EnsureCreatedAsync();

            Db = dbContext;
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _container.DisposeAsync();
        }

        public void ResetDatabase()
        {
            Db.Database.ExecuteSql($"DELETE FROM Enrolments");
            Db.Database.ExecuteSql($"DELETE FROM Addresses");
            Db.Database.ExecuteSql($"DELETE FROM Students");
        }
    }
}