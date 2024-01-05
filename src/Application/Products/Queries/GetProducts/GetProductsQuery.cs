using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Products.Queries.GetProducts
{
    public sealed record GetProductsQuery(LoadOptions LoadOptions)
        : IQuery<PagedList<ProductsResponse>>;
}
