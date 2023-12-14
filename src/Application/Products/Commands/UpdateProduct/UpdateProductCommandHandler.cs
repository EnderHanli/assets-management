using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null || product.IsDeleted)
            {
                return Result.Failure(AssetErrors.ProductNotFound);
            }

            var category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (category == null || category.IsDeleted)
            {
                return Result.Failure(AssetErrors.CategoryNotFound);
            }

            if (request.ManufacturerId.HasValue)
            {
                var manufacturer = await _context.Manufacturers.FindAsync(new object[] { request.ManufacturerId.Value }, cancellationToken);
                if (manufacturer == null || manufacturer.IsDeleted)
                {
                    return Result.Failure(AssetErrors.ManufacturerNotFound);
                }
                product.ManufacturerId = manufacturer.Id;
            }


            product.PurchaseCost = request.PurchaseCost;
            product.IsArchived = request.IsArchived;
            product.CategoryId = request.CategoryId;
            product.ModelNo = request.ModelNo;
            product.Name = request.Name;
            product.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
