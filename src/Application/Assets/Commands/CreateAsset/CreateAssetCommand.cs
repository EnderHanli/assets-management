using Application.Common.Messaging;

namespace Application.Assets.Commands.CreateAsset
{
    public sealed record CreateAssetCommand(
        string Code,
        int ProductId,
        int StatusId,
        int? DepartmentId,
        string? Name,
        string? SerialNumber,
        DateTime? WarrantyExpirationDate,
        DateTime? PurchaseDate,
        decimal? PurchaseCost,
        int? ScheduleControlTimeId,
        string? Notes)
        : ICommand<int>;
}
