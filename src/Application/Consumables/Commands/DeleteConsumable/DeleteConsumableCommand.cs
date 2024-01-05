using Application.Common.Messaging;

namespace Application.Consumables.Commands.DeleteConsumable
{
    public sealed record DeleteConsumableCommand(int Id) : ICommand;
}
