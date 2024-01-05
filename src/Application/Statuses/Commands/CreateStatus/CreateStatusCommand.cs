using Application.Common.Messaging;

namespace Application.Statuses.Commands.CreateStatus
{
    public sealed record CreateStatusCommand(
        string Name,
        int StatusTypeId,
        string? Notes)
        : ICommand<int>;
}
