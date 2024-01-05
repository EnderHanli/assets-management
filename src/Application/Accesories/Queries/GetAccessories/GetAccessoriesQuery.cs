using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Accesories.Queries.GetAccessories
{
    public sealed record GetAccessoriesQuery(LoadOptions LoadOptions) : IQuery<PagedList<AccessoryResponse>>;
}
