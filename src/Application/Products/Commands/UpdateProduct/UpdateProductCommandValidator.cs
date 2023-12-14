using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty()
                .MustAsync(BeUniqueName).WithMessage("Product already exists.");
        }

        public async Task<bool> BeUniqueName(UpdateProductCommand command, string name, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Where(p => p.Id != command.Id)
                .AllAsync(p => p.Name != name, cancellationToken);
        }
    }
}
