using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Consumables.Commands.UpdateConsumable
{
    internal sealed class UpdateConsumableCommandHandler : ICommandHandler<UpdateConsumableCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateConsumableCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateConsumableCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FindAsync(new object[] { request.CategoryId }, cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.CategoryNotFound);
            }

            if (request.ManufacturerId.HasValue)
            {
                var manufacturer = await _context.Manufacturers
                    .FindAsync(new object[] { request.ManufacturerId.Value }, cancellationToken);
                if (manufacturer is null || manufacturer.IsDeleted)
                {
                    return Result.Failure<int>(AssetErrors.ManufacturerNotFound);
                }
            }

            if (request.DepartmentId.HasValue)
            {
                var department = await _context.Departments
                    .FindAsync(new object[] { request.DepartmentId }, cancellationToken);
                if (department is null || department.IsDeleted)
                {
                    return Result.Failure<int>(AssetErrors.DepartmentNotFound);
                }
            }

            var consumable = await _context.Consumables
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (consumable is null || consumable.IsDeleted)
            {
                return Result.Failure(AssetErrors.ConsumableNotFound);
            }

            consumable.CategoryId = request.CategoryId;
            consumable.ManufacturerId = request.ManufacturerId;
            consumable.DepartmentId = request.DepartmentId;
            consumable.ModelNo = request.ModelNo;
            consumable.Name = request.Name;
            consumable.Notes = request.Notes;
            consumable.PurchaseCost = request.PurchaseCost;
            consumable.PurchaseDate = request.PurchaseDate;
            consumable.Quantity = request.Quantity;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
