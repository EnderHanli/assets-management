using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Components.Commands.DeleteComponent
{
    internal sealed class DeleteComponentCommandHandler : ICommandHandler<DeleteComponentCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteComponentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteComponentCommand request, CancellationToken cancellationToken)
        {
            var compoent = await _context.Components.FindAsync(new object[] { request.Id }, cancellationToken);
            if (compoent is null || compoent.IsDeleted)
            {
                return Result.Failure(AssetErrors.ComponentNotFound);
            }

            //TODO: Add using component component.

            _context.Components.Remove(compoent);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
