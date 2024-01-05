using FluentValidation;

namespace Application.Consumables.Commands.CreateConsumable
{
    internal sealed class CreateConsumableCommandValidator : AbstractValidator<CreateConsumableCommand>
    {
        public CreateConsumableCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
