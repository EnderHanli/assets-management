using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Components.Queries.GetComponents
{
    public sealed record GetComponentsQuery(LoadOptions LoadOptions) : IQuery<PagedList<ComponentResponse>>;
}
