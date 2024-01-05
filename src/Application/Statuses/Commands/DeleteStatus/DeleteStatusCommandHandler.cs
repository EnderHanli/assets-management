using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Statuses.Commands.DeleteStatus
{
    internal sealed class DeleteStatusCommandHandler : ICommandHandler<DeleteStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var status = await _context.Statuses
                .Include(p => p.Assets)
                .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (status is null || status.IsDeleted)
            {
                return Result.Failure(AssetErrors.StatusNotFound);
            }

            if (status.IsSystem)
            {
                return Result.Failure(AssetErrors.StatusSystemValue);
            }

            if (status.Assets.Count > 0)
            {
                return Result.Failure(AssetErrors.StatusCurrentlyUsed);
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
