using Application.Common.Messaging;

namespace Application.Products.Queries.GetProducts
{
    public sealed record GetProductsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int PageIndex = 1,
        int PageSize = 10)
        : IQuery<List<ProductsResponse>>;
}
