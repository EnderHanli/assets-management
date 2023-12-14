using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Categories.Commands.UpdateCategory
{
    internal sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (category is null || category.IsDeleted)
            {
                return Result.Failure(AssetErrors.NotFound);
            }

            category.Name = request.Name;
            category.CategoryType = request.CategoryType;
            category.Notes = request.Notes;
            category.SendEmailNotification = request.SendEmailNotification;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
