using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Categories.Queries.GetCategories
{
    public sealed record GetCategoriesQuery(LoadOptions LoadOptions)
        : IQuery<PagedList<CategoryResponse>>;
}
