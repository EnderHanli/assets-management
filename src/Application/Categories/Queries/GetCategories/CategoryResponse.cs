using Domain.Enums;

namespace Application.Categories.Queries.GetCategories
{
    public sealed record CategoryResponse(
        int Id,
        string Name,
        CategoryType Type,
        bool SendEmailNotification,
        int Quantity);
}
