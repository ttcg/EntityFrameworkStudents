using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Students.Repository;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Students.IntegrationTests;

namespace Students.IntegrationTests
{
    public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public StudentsDbContext Db { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(IDbContextOptionsConfiguration<StudentsDbContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<StudentsDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });

            builder.UseEnvironment("Development");
        }

        public async Task InitializeAsync()
        {
            var dbContext = Services.CreateScope().ServiceProvider.GetRequiredService<StudentsDbContext>();
            dbContext.Database.OpenConnection();
            await dbContext.Database.EnsureCreatedAsync();

            Db = dbContext;            
        }

        Task IAsyncLifetime.DisposeAsync()
        {           
            return Task.CompletedTask;
        }

        public void ResetDatabase()
        {
            Db.Database.ExecuteSql($"DELETE FROM Enrolments");
            Db.Database.ExecuteSql($"DELETE FROM Addresses");
            Db.Database.ExecuteSql($"DELETE FROM Students");
        }
    }
}

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<IntegrationTestFactory>
{
    // Class used only for Collection Definition
}