using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Accesories.Commands.UpdateAccessory
{
    internal sealed class UpdateAccessoryCommandHandler : ICommandHandler<UpdateAccessoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAccessoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateAccessoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure(AssetErrors.CategoryNotFound);
            }

            if (request.ManufacturerId.HasValue)
            {
                var manufacturer = await _context.Manufacturers
                    .FindAsync(new object[] { request.ManufacturerId.Value }, cancellationToken);
                if (manufacturer is null || manufacturer.IsDeleted)
                {
                    return Result.Failure(AssetErrors.ManufacturerNotFound);
                }
            }

            if (request.DepartmentId.HasValue)
            {
                var department = await _context.Manufacturers
                    .FindAsync(new object[] { request.DepartmentId.Value }, cancellationToken);
                if (department is null || department.IsDeleted)
                {
                    return Result.Failure(AssetErrors.DepartmentNotFound);
                }
            }

            var accessory = await _context.Accessories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (accessory is null || accessory.IsDeleted)
            {
                return Result.Failure(AssetErrors.AccessoryNotFound);
            }

            accessory.CategoryId = request.CategoryId;
            accessory.DepartmentId = request.DepartmentId;
            accessory.ManufacturerId = request.ManufacturerId;
            accessory.ModelNo = request.ModelNo;
            accessory.Name = request.Name;
            accessory.Notes = request.Notes;
            accessory.PurchaseCost = request.PurchaseCost;
            accessory.PurchaseDate = request.PurchaseDate;
            accessory.Quantity = request.Quantity;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
