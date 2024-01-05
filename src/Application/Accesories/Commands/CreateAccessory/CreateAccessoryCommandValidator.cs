using FluentValidation;

namespace Application.Accesories.Commands.CreateAccessory
{
    public class CreateAccessoryCommandValidator : AbstractValidator<CreateAccessoryCommand>
    {
        public CreateAccessoryCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
