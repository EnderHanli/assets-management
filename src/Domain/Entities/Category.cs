using Domain.Common;

namespace Domain.Entities
{
    public class Category : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; } = null!;
        public string? Notes { get; set; }
        public bool SendEmailNotification { get; set; }
        public int Quantity { get; set; } = 0;
        public List<Accessory> Accessories { get; set; } = new();
        public List<Component> Components { get; set; } = new();
        public List<Consumable> Consumables { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<License> Licenses { get; set; } = new();
    }
}
