using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
        int Id,
        string Name,
        CategoryType CategoryType,
        bool SendEmailNotification,
        string? Notes)
        : ICommand;
}
