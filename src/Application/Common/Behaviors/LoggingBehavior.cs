using Application.Common.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    public class LoggingBehavior<TCommand, TResponse>
        : IPipelineBehavior<TCommand, TResponse>
        where TCommand : IBaseCommand

    {
        private readonly ILogger<TCommand> _logger;

        public LoggingBehavior(ILogger<TCommand> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TCommand request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;
            try
            {
                _logger.LogInformation("Executing command {Command}", commandName);

                var result = await next();

                _logger.LogInformation("Command {Command} processed successfully", commandName);

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Command {Command} processing failed", commandName);
                throw;
            }
        }
    }
}
