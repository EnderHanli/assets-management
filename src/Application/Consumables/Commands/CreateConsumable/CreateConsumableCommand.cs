using Application.Common.Messaging;

namespace Application.Consumables.Commands.CreateConsumable
{
    public sealed record CreateConsumableCommand(
        string Name,
        int CategoryId,
        int? ManufacturerId,
        int? DepartmentId,
        string? ModelNo,
        DateTime? PurchaseDate,
        decimal? PurchaseCost,
        int Quantity,
        string? Notes)
        : ICommand<int>;
}
