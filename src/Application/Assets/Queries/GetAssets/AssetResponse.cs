namespace Application.Assets.Queries.GetAssets
{
    public sealed record AssetResponse(
        int Id,
        string Code,
        string ProductName,
        string StatusName,
        string? Name,
        string? SerialNumber,
        DateTime? WarrantyExpirationDate,
        DateTime? PurchaseDate,
        decimal? PuchaseCost,
        string CategoryName,
        string? ManufacturerName,
        string? DepartmentName,
        string? Notes);
}
