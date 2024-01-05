using FluentValidation;

namespace Application.Consumables.Commands.UpdateConsumable
{
    internal sealed class UpdateConsumableCommandValidator : AbstractValidator<UpdateConsumableCommand>
    {
        public UpdateConsumableCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
