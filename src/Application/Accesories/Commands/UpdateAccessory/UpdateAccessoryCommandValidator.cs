using FluentValidation;

namespace Application.Accesories.Commands.UpdateAccessory
{
    internal sealed class UpdateAccessoryCommandValidator : AbstractValidator<UpdateAccessoryCommand>
    {
        public UpdateAccessoryCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
