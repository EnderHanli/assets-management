using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.CreateProduct
{
    internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .SingleOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken: cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.CategoryNotFound);
            }

            var product = new Product
            {
                CategoryId = category.Id,
                Name = request.Name,
                IsArchived = request.IsArchived,
                ManufacturerId = request.ManufacturerId,
                ModelNo = request.ModelNo,
                Notes = request.Notes,
                PurchaseCost = request.PurchaseCost,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
