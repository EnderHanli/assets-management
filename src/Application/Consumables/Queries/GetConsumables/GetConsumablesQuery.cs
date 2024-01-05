using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Consumables.Queries.GetConsumables
{
    public sealed record GetConsumablesQuery(LoadOptions LoadOptions) : IQuery<PagedList<ConsumableResponse>>;
}
