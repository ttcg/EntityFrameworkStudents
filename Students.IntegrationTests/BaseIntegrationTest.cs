using System.Text.Json.Serialization;
using System.Text.Json;
using Students.Repository;

namespace Students.IntegrationTests
{
    public class BaseIntegrationTest : IAsyncLifetime
    {
        protected HttpClient _client;
        protected StudentsDbContext _db;
        private Func<Task> _resetDatabase;
        

        protected readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        protected async Task<T> GetDtoFromResponse<T>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseBody, JsonSerializerOptions);
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;            
        }

        public Task DisposeAsync()
        {
            return _resetDatabase();
        }

        public BaseIntegrationTest(IntegrationTestFactory factory)
        {
            _client = factory.CreateDefaultClient();
            _resetDatabase = factory.ResetDatabaseByUsingRespawn;
            _db = factory.Db;
        }
    }
}
