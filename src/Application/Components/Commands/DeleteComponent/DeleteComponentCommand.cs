using Application.Common.Messaging;

namespace Application.Components.Commands.DeleteComponent
{
    public sealed record DeleteComponentCommand(int Id) : ICommand;
}
