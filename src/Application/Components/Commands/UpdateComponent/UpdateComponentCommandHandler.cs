using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Components.Commands.UpdateComponent
{
    internal sealed class UpdateComponentCommandHandler : ICommandHandler<UpdateComponentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateComponentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure(AssetErrors.CategoryNotFound);
            }

            if (request.ManufacturerId.HasValue)
            {
                var manufacturer = await _context.Manufacturers.FindAsync(new object[] { request.ManufacturerId.Value }, cancellationToken);
                if (manufacturer is null || manufacturer.IsDeleted)
                {
                    return Result.Failure(AssetErrors.ManufacturerNotFound);
                }
            }

            if (request.DepartmentId.HasValue)
            {
                var department = await _context.Departments.FindAsync(new object[] { request.DepartmentId.Value }, cancellationToken);
                if (department is null || department.IsDeleted)
                {
                    return Result.Failure(AssetErrors.DepartmentNotFound);
                }
            }

            var component = await _context.Components.FindAsync(new object[] { request.Id }, cancellationToken);
            if (component is null || component.IsDeleted)
            {
                return Result.Failure(AssetErrors.ComponentNotFound);
            }

            component.ManufacturerId = request.ManufacturerId;
            component.DepartmentId = request.DepartmentId;
            component.CategoryId = request.CategoryId;
            component.ModelNo = request.ModelNo;
            component.Name = request.Name;
            component.Notes = request.Notes;
            component.PurchaseCost = request.PurchaseCost;
            component.PurchaseDate = request.PurchaseDate;
            component.Quantity = request.Quantity;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
