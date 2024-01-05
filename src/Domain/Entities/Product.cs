using Domain.Common;

namespace Domain.Entities
{
    public class Product : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int? ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public string? ModelNo { get; set; }
        public decimal? PurchaseCost { get; set; }
        public string? Notes { get; set; }
        public bool IsArchived { get; set; }
        public List<Asset> Assets { get; set; } = new();
    }
}
