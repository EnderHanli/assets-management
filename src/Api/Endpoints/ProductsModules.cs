using Application.Common.Models;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProducts;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class ProductsModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/products");

            builder.MapGet("", GetProducts);
            builder.MapPost("", CreateProduct);
            builder.MapPut("{id:int}", UpdateProduct);
            builder.MapDelete("{id:int}", DeleteProduct);
        }

        public async Task<IResult> GetProducts(
            ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetProductsQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateProduct(ISender sender, CreateProductCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return TypedResults.BadRequest();
            }

            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.NoContent();
        }

        public async Task<IResult> DeleteProduct(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.NoContent();
        }
    }
}
