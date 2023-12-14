using Application.Common.Messaging;

namespace Application.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
        int CategoryId,
        int? ManufacturerId,
        string? ModelNo,
        decimal? PurchaseCost,
        string? Notes,
        bool IsArchived)
        : ICommand<int>;
}
