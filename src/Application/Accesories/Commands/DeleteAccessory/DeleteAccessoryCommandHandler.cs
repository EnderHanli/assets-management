using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Accesories.Commands.DeleteAccessory
{
    internal sealed class DeleteAccessoryCommandHandler : ICommandHandler<DeleteAccessoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAccessoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteAccessoryCommand request, CancellationToken cancellationToken)
        {
            var accessory = await _context.Accessories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (accessory is null || accessory.IsDeleted)
            {
                return Result.Failure(AssetErrors.AccessoryNotFound);
            }

            _context.Accessories.Remove(accessory);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
