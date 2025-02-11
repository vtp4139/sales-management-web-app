namespace SalesManagementWebsite.Domain.Entities
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
