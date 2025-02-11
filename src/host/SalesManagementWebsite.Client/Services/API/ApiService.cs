using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class ApiService : IApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient();
        var token = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return client;
    }

    public async Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, object? requestData = null)
    {
        var client = CreateClient();
        var request = new HttpRequestMessage(method, $"{_configuration["BackendUrl:Default"]}{endpoint}");

        if (requestData != null)
        {
            var json = JsonSerializer.Serialize(requestData);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API call failed: {response.StatusCode}, Error: {errorMessage}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent);
    }
}
