using Application.Common.Messaging;

namespace Application.Statuses.Commands.UpdateStatus
{
    public sealed record UpdateStatusCommand(int Id, string Name, int StatusTypeId, string? Notes) : ICommand
    {
    }
}
