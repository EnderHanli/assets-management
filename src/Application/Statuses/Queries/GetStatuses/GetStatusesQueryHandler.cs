using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Statuses.Queries.GetStatuses
{
    internal sealed class GetStatusesQueryHandler : IQueryHandler<GetStatusesQuery, PagedList<StatusResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetStatusesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PagedList<StatusResponse>>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Status> statusesQuery = _context.Statuses
                .Include(p => p.StatusType);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                statusesQuery = statusesQuery.Where(p =>
                    p.Name.Contains(request.LoadOptions.SearchTerm) ||
                    p.StatusType.Name.Contains(request.LoadOptions.SearchTerm));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                statusesQuery = statusesQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                statusesQuery = statusesQuery.OrderBy(GetSortProperty(request));
            }

            var statuses = statusesQuery
                .Select(p =>
                    new StatusResponse(
                        p.Id,
                        p.Name,
                        p.StatusType.Id,
                        p.StatusType.Name,
                        p.IsSystem,
                        p.Assets.Count));

            var result = await PagedList<StatusResponse>
                .CreateAsync(statuses, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);

            return result;
        }

        private static Expression<Func<Status, object>> GetSortProperty(GetStatusesQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "name" => status => status.Name,
            "type" => status => status.StatusType.Name,
            _ => product => product.Id
        };
    }
}
