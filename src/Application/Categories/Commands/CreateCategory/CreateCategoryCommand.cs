using Application.Common.Messaging;

namespace Application.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(
        string Name,
        int CategoryTypeId,
        bool SendEmailNotification,
        string? Notes)
        : ICommand<int>;
}
