using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Statuses.Commands.UpdateStatus
{
    internal sealed class UpdateStatusCommandHandler : ICommandHandler<UpdateStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var statusType = await _context.StatusTypes.FindAsync(new object[] { request.StatusTypeId }, cancellationToken);
            if (statusType is null)
            {
                return Result.Failure(AssetErrors.StatusTypeNotFound);
            }

            var status = await _context.Statuses.FindAsync(new object[] { request.Id }, cancellationToken);
            if (status is null || status.IsDeleted)
            {
                return Result.Failure(AssetErrors.StatusNotFound);
            }

            status.Name = request.Name;
            status.StatusTypeId = request.StatusTypeId;
            status.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
