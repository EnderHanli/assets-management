using Application.Common.Messaging;

namespace Application.Consumables.Commands.UpdateConsumable
{
    public sealed record UpdateConsumableCommand(
        int Id,
        string Name,
        int CategoryId,
        int? ManufacturerId,
        int? DepartmentId,
        string? ModelNo,
        DateTime? PurchaseDate,
        decimal? PurchaseCost,
        int Quantity,
        string? Notes)
        : ICommand;
}
