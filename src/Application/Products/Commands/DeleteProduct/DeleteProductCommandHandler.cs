using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Assets)
                .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null || product.IsDeleted)
            {
                return Result.Failure(AssetErrors.ProductNotFound);
            }

            if (product.Assets.Count > 0)
            {
                return Result.Failure(AssetErrors.ProductCurrentlyUsed);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
