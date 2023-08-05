using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;

namespace Awesome.BusinessService.Utilities
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content) ?? throw new JsonException("Deserialization returned null.");
            }
            catch
            {
                throw;
            }
        }
    }
}
