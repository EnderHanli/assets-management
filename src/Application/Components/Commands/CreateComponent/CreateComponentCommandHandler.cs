using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Components.Commands.CreateComponent
{
    internal sealed class CreateComponentCommandHandler : ICommandHandler<CreateComponentCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateComponentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<int>> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.CategoryNotFound);
            }

            if (request.ManufacturerId.HasValue)
            {
                var manufacturer = await _context.Manufacturers.FindAsync(new object[] { request.ManufacturerId.Value }, cancellationToken);
                if (manufacturer is null || manufacturer.IsDeleted)
                {
                    return Result.Failure<int>(AssetErrors.ManufacturerNotFound);
                }
            }

            if (request.DepartmentId.HasValue)
            {
                var department = await _context.Departments.FindAsync(new object[] { request.DepartmentId.Value }, cancellationToken);
                if (department is null || department.IsDeleted)
                {
                    return Result.Failure<int>(AssetErrors.DepartmentNotFound);
                }
            }

            var component = new Component
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

            _context.Components.Add(component);
            await _context.SaveChangesAsync(cancellationToken);

            return component.Id;
        }
    }
}
