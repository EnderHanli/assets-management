using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MustAsync(BeUniqueName).WithMessage("'{PropertyName}' mus be unique.");
        }

        private async Task<bool> BeUniqueName(UpdateCategoryCommand command, string name, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(p => p.Id != command.Id)
                .AllAsync(p => p.Name != name, cancellationToken);
        }
    }
}
