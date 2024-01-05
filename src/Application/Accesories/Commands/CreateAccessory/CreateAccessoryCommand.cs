using Application.Common.Messaging;

namespace Application.Accesories.Commands.CreateAccessory
{
    public sealed record CreateAccessoryCommand(
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
