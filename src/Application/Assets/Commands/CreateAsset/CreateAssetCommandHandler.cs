using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Assets.Commands.CreateAsset
{
    internal sealed class CreateAssetCommandHandler : ICommandHandler<CreateAssetCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateAssetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<int>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
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

            var asset = new Asset
            {
                Code = request.Code,
                DepartmentId = request.DepartmentId,
                Name = request.Name,
                Notes = request.Notes,
                ProductId = request.ProductId,
                PurchaseCost = request.PurchaseCost,
                PurchaseDate = request.PurchaseDate,
                ScheduleControlTimeId = request.ScheduleControlTimeId,
                SerialNumber = request.SerialNumber,
                StatusId = request.StatusId,
                WarrantyExpirationDate = request.WarrantyExpirationDate
            };

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync(cancellationToken);

            return asset.Id;
        }
    }
}
