using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Assets.Commands.UpdateAsset
{
    internal sealed class UpdateAssetCommandHandler : ICommandHandler<UpdateAssetCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAssetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
            if (product is null || product.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.ProductNotFound);
            }
            var status = await _context.Statuses.FindAsync(new object[] { request.StatusId }, cancellationToken);
            if (status is null || status.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.StatusNotFound);
            }

            if (request.ScheduleControlTimeId.HasValue)
            {
                var scheduleControlTime = await _context.ControlTimeTypes
                    .FindAsync(new object[] { request.ScheduleControlTimeId }, cancellationToken);
                if (scheduleControlTime is null)
                {
                    return Result.Failure<int>(AssetErrors.ControlTimeTypeNotFound);
                }
            }

            var asset = await _context.Assets.FindAsync(new object[] { request.Id }, cancellationToken);
            if (asset is null || asset.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.AssetNotFound);
            }

            asset.Code = request.Code;
            asset.Name = request.Name;
            asset.DepartmentId = request.DepartmentId;
            asset.Notes = request.Notes;
            asset.ProductId = request.ProductId;
            asset.PurchaseCost = request.PurchaseCost;
            asset.PurchaseDate = request.PurchaseDate;
            asset.ScheduleControlTimeId = request.ScheduleControlTimeId;
            asset.SerialNumber = request.SerialNumber;
            asset.StatusId = request.StatusId;
            asset.WarrantyExpirationDate = request.WarrantyExpirationDate;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
