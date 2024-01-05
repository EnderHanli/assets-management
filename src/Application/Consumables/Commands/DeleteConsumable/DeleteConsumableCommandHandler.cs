using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Consumables.Commands.DeleteConsumable
{
    internal sealed class DeleteConsumableCommandHandler : ICommandHandler<DeleteConsumableCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteConsumableCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteConsumableCommand request, CancellationToken cancellationToken)
        {
            var consumable = await _context.Consumables
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (consumable is null || consumable.IsDeleted)
            {
                return Result.Failure(AssetErrors.ConsumableNotFound);
            }

            _context.Consumables.Remove(consumable);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
