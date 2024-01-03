using System.Text.Json.Serialization;

namespace SalesManagementWebsite.Contracts.Dtos.Response
{
    public class ResponseHandle<T>
    {
        public bool IsSuccess { get; set; }       
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<T>? ListData { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }
    }
}
