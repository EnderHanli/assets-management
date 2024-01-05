using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Statuses.Commands.UpdateStatus
{
    public sealed class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStatusCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MustAsync(BeUniqueName).WithMessage("Status already exists.");
        }

        private async Task<bool> BeUniqueName(UpdateStatusCommand command, string name, CancellationToken cancellationToken)
        {
            return await _context.Statuses
                .Where(p => p.Id == command.Id)
                .AllAsync(p => p.Name != name);
        }
    }
}
