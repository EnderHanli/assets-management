using Domain.Entities;

namespace Application.Products.Queries.GetProducts
{
    public record ProductsResponse(
        int Id,
        string Name,
        CategoryResponse CategoryResponse,
        string? ModelNo,
        Manufacturer? ManufacturerResponse,
        decimal? PurchaseCost,
        string? Notes,
        bool IsArchived,
        int AssetsCount);

    public record CategoryResponse(int Id, string Name);
    public record ManufacturerResponse(int Id, string Name);
}
