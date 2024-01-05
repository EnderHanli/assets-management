using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Statuses.Commands.CreateStatus
{
    internal sealed class CreateStatusCommandHandler : ICommandHandler<CreateStatusCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<int>> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            var statusType = await _context.StatusTypes.FindAsync(new object[] { request.StatusTypeId }, cancellationToken);
            if (statusType is null)
            {
                return Result.Failure<int>(AssetErrors.StatusTypeNotFound);
            }

            var status = new Status
            {
                Name = request.Name,
                StatusTypeId = request.StatusTypeId,
                Notes = request.Notes,
                IsSystem = false
            };

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync(cancellationToken);

            return status.Id;
        }
    }
}
