using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Assets.Queries.GetAssets
{
    public sealed record GetAssetsQuery(LoadOptions LoadOptions) : IQuery<PagedList<AssetResponse>>;
}
