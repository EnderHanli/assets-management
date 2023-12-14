
using Application.Common.Messaging;

namespace Application.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(int Id) : ICommand;
}
