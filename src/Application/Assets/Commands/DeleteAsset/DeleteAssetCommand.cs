using Application.Common.Messaging;

namespace Application.Assets.Commands.DeleteAsset
{
    public sealed record DeleteAssetCommand(int Id) : ICommand;
}
