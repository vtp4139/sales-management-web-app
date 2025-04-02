public interface IApiService
{
    Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, object? requestData = null);
}
