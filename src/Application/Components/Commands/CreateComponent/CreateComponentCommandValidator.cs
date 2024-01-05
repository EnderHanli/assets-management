using FluentValidation;

namespace Application.Components.Commands.CreateComponent
{
    internal sealed class CreateComponentCommandValidator : AbstractValidator<CreateComponentCommand>
    {
        public CreateComponentCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
