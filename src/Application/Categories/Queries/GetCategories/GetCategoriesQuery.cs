using Application.Common.Messaging;

namespace Application.Categories.Queries.GetCategories
{
    public sealed record GetCategoriesQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int PageNumber = 1,
        int PageSize = 10)
        : IQuery<List<CategoryResponse>>;
}
