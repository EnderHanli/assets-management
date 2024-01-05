using Application.Common.Messaging;

namespace Application.Assets.Commands.UpdateAsset
{
    public sealed record UpdateAssetCommand(
        int Id,
        string Code,
        int ProductId,
        int StatusId,
        int DepartmentId,
        string? Name,
        string? SerialNumber,
        DateTime? WarrantyExpirationDate,
        DateTime? PurchaseDate,
        decimal? PurchaseCost,
        int? ScheduleControlTimeId,
        string? Notes)
        : ICommand;
}
