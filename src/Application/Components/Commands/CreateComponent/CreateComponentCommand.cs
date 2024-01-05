using Application.Common.Messaging;

namespace Application.Components.Commands.CreateComponent
{
    public sealed record CreateComponentCommand(
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
