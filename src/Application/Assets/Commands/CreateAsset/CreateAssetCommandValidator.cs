using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Assets.Commands.CreateAsset
{
    public sealed class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateAssetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Code)
                .NotEmpty()
                .MustAsync(BeUniqueCode).WithMessage("Asset code already exists.");
        }

        private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
        {
            return await _context.Assets
                .AllAsync(p => p.Code != code, cancellationToken);
        }
    }
}
