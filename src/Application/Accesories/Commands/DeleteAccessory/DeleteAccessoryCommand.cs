using Application.Common.Messaging;

namespace Application.Accesories.Commands.DeleteAccessory
{
    public sealed record DeleteAccessoryCommand(int Id) : ICommand;
}
