using Application.Common.Messaging;

namespace Application.Components.Commands.UpdateComponent
{
    public sealed record UpdateComponentCommand(
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
