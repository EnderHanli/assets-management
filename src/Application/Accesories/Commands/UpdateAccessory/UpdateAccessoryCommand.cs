using Application.Common.Messaging;

namespace Application.Accesories.Commands.UpdateAccessory
{
    public sealed record UpdateAccessoryCommand(
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
