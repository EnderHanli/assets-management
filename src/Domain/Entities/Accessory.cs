using Domain.Common;

namespace Domain.Entities
{
    public class Accessory : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int? ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? ModelNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchaseCost { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
