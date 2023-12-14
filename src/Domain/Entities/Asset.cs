using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Asset : FullAuditableEntity
    {
        public string Code { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; } = new();
        public int StatusId { get; set; }
        public Status Status { get; set; } = new();
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? WarrantyExpirationDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchaseCost { get; set; }
        public ControlTimeType? ScheduleControlTime { get; set; }
        public string? Notes { get; set; }
    }
}
