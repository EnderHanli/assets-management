using Domain.Entities;

namespace Application.Components.Queries.GetComponents
{
    public sealed record ComponentResponse
    {

        public ComponentResponse(int id,
        string name,
        int quantity,
        string? modelNo,
        DateTime? purchaseDate,
        decimal? purchaseCost,
        Category category,
        Manufacturer? manufacturer,
        Department? department,
        string? notes)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            ModelNo = modelNo;
            PurchaseDate = purchaseDate;
            PurchaseCost = purchaseCost;
            CategoryId = category.Id;
            CategoryName = category.Name;
            ManufacturerId = manufacturer?.Id;
            ManufacturerName = manufacturer?.Name;
            DepartmentId = department?.Id;
            DepartmentName = department?.Name;
            Notes = notes;
        }

        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; }
        public string? ModelNo { get; }
        public DateTime? PurchaseDate { get; }
        public decimal? PurchaseCost { get; }
        public int CategoryId { get; }
        public string CategoryName { get; }
        public int? ManufacturerId { get; }
        public string? ManufacturerName { get; }
        public int? DepartmentId { get; }
        public string? DepartmentName { get; }
        public string? Notes { get; }
    }
}
