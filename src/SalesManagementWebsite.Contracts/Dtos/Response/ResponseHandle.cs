
namespace SalesManagementWebsite.Contracts.Dtos.Response
{
    public class ResponseHandle<T>
    {
        public bool IsSuccess { get; set; }       
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
