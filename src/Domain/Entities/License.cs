using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class License : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new();
        public int? ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? LicensedToName { get; set; }
        public string? LicensedToEmail { get; set; }
        public LicenseType LicenseType { get; set; }
        public int Seat { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchaseCost { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Notes { get; set; }
    }
}
