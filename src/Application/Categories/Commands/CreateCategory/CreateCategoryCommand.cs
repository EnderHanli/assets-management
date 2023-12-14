using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(
        string Name,
        CategoryType CategoryType,
        bool SendEmailNotification,
        string? Notes)
        : ICommand<int>;
}
