using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Consumables.Commands.CreateConsumable
{
    internal sealed class CreateConsumableCommandHandler : ICommandHandler<CreateConsumableCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateConsumableCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(CreateConsumableCommand request, CancellationToken cancellationToken)
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

            var consumable = new Consumable
            {
                CategoryId = request.CategoryId,
                ManufacturerId = request.ManufacturerId,
                DepartmentId = request.DepartmentId,
                ModelNo = request.ModelNo,
                Name = request.Name,
                Notes = request.Notes,
                PurchaseCost = request.PurchaseCost,
                PurchaseDate = request.PurchaseDate,
                Quantity = request.Quantity,
            };

            _context.Consumables.Add(consumable);
            await _context.SaveChangesAsync(cancellationToken);

            return consumable.Id;
        }
    }
}
