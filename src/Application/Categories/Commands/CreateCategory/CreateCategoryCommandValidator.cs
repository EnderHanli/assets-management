using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateCategoryCommandValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MustAsync(BeUniqueName).WithMessage("'{PropertyName}' must be unique.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AllAsync(p => p.Name != name, cancellationToken);
        }
    }
}
