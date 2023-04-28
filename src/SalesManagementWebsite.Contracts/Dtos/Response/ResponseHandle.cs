
namespace SalesManagementWebsite.Contracts.Dtos.Response
{
    public class ResponseHandle<T>
    {
        public bool IsSuccess { get; set; }       
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public IEnumerable<T> ListData { get; set; } = new List<T>();
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
