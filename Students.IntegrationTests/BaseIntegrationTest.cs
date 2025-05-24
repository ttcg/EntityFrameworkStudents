using System.Text.Json.Serialization;
using System.Text.Json;

namespace Students.IntegrationTests
{
    public class BaseIntegrationTest
    {
        protected HttpClient _client;

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

        public BaseIntegrationTest(IntegrationTestFactory factory)
        {
            _client = factory.CreateDefaultClient();
            factory.ResetDatabase();
        }


    }
}
