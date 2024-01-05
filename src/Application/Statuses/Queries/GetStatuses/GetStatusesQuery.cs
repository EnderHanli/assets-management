using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Statuses.Queries.GetStatuses
{
    public sealed record GetStatusesQuery(LoadOptions LoadOptions) : IQuery<PagedList<StatusResponse>>;
}
