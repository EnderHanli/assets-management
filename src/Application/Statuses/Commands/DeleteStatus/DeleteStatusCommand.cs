using Application.Common.Messaging;

namespace Application.Statuses.Commands.DeleteStatus
{
    public sealed record DeleteStatusCommand(int Id) : ICommand;
}
