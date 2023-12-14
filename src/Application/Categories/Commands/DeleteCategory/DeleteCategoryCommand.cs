using Application.Common.Messaging;

namespace Application.Categories.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(int Id) : ICommand;
}
