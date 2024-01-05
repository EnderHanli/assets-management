using Application.Common.Messaging;
using Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TCommand, TResponse>
        : IPipelineBehavior<TCommand, TResponse>
        where TCommand : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TCommand>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TCommand>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(
            TCommand request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {

                var context = new ValidationContext<TCommand>(request);

                var validationResults = await Task.WhenAll(
                   _validators
                        .Select(validator => validator.ValidateAsync(context, cancellationToken)));

                var validationErrors = validationResults
                    .Where(validationResult => validationResult.Errors.Any())
                    .SelectMany(validationResult => validationResult.Errors)
                    .Select(validationFailure => new ValidationError(validationFailure.PropertyName, validationFailure.ErrorMessage))
                        .ToList();

                if (validationErrors.Any())
                {
                    throw new Exceptions.ValidationException(validationErrors);
                }
            }

            return await next();
        }
    }
}
