namespace Application.Categories.Queries.GetCategories
{
    public sealed record CategoryResponse(
        int Id,
        string Name,
        string Type,
        bool SendEmailNotification,
        int Quantity);
}
