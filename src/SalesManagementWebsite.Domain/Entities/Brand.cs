﻿
namespace SalesManagementWebsite.Domain.Entities
{
    public class Brand : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual List<Item> Items { get; set; } = new List<Item>();
    }
}
