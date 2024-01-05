using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Statuses.Commands.CreateStatus
{
    public sealed class CreateStatusCommandValidator : AbstractValidator<CreateStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateStatusCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MustAsync(BeUniqueName).WithMessage("Status already exists.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Statuses
                .AllAsync(p => p.Name != name, cancellationToken);
        }
    }
}
