using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Categories.Commands.DeleteCategory
{
    internal sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure(AssetErrors.CategoryNotFound);
            }

            if (category.Quantity > 0)
            {
                return Result.Failure(AssetErrors.CategoryCurrentlyUsed);
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
