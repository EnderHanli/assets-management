using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Assets.Commands.DeleteAsset
{
    internal sealed class DeleteAssetCommandHandler : ICommandHandler<DeleteAssetCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAssetCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _context.Assets.FindAsync(new object[] { request.Id }, cancellationToken);
            if (asset is null || asset.IsDeleted)
            {
                return Result.Failure<int>(AssetErrors.AssetNotFound);
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
