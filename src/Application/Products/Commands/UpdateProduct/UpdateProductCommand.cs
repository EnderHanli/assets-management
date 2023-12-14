using Application.Common.Messaging;

namespace Application.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        int Id,
        string Name,
        int CategoryId,
        int? ManufacturerId,
        string? ModelNo,
        decimal? PurchaseCost,
        string? Notes,
        bool IsArchived)
        : ICommand;
}
