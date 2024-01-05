using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Assets.Commands.UpdateAsset
{
    public sealed class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAssetCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Code)
                .NotEmpty()
                .MustAsync(BeUniqueCode).WithMessage("Asset code already exists.");
        }

        private async Task<bool> BeUniqueCode(UpdateAssetCommand command, string code, CancellationToken cancellationToken)
        {
            return await _context.Assets
                .Where(p => p.Id != command.Id)
                .AllAsync(p => p.Code != code, cancellationToken);
        }
    }
}
