using FluentValidation;

namespace Application.Components.Commands.UpdateComponent
{
    internal sealed class UpdateCompnentCommandValidator : AbstractValidator<UpdateComponentCommand>
    {
        public UpdateCompnentCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
        }
    }
}
