using Application.Common.Messaging;

namespace Application.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
        int Id,
        string Name,
        int CategoryTypeId,
        bool SendEmailNotification,
        string? Notes)
        : ICommand;
}
