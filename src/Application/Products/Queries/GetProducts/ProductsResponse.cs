using Domain.Entities;

namespace Application.Products.Queries.GetProducts
{
    public record ProductsResponse
    {
        public ProductsResponse(
        int id,
        string name,
        string? modelNo,
        Category category,
        Manufacturer? manufacturer,
        decimal? purchaseCost,
        string? notes,
        bool isArchived,
        int assetsCount)
        {
            Id = id;
            Name = name;
            ModelNo = modelNo;
            PurchaseCost = purchaseCost;
            Notes = notes;
            IsArchived = isArchived;
            AssetsCount = assetsCount;
            Category = new CategoryResponse(category.Id, category.Name);
            Manufacturer = manufacturer is not null ? new ManufacturerResponse(manufacturer.Id, manufacturer.Name)
           : null;
        }

        public int Id { get; }
        public string Name { get; }
        public string? ModelNo { get; }
        public decimal? PurchaseCost { get; }
        public string? Notes { get; }
        public bool IsArchived { get; }
        public int AssetsCount { get; }
        public CategoryResponse Category { get; }
        public ManufacturerResponse? Manufacturer { get; }
    }

    public record CategoryResponse(int Id, string Name);
    public record ManufacturerResponse(int Id, string Name);
}
