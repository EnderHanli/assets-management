using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries.GetCategories;
using Application.Common.Models;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class CategoriesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/categories");

            builder.MapGet("", GetCategories);
            builder.MapPost("", CreateCategory);
            builder.MapPut("{id:int}", UpdateCategory);
            builder.MapDelete("{id:int}", DeleteCategory);
        }

        public async Task<IResult> GetCategories(ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetCategoriesQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateCategory(ISender sender, CreateCategoryCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateCategory(ISender sender, int id, UpdateCategoryCommand command)
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

        public async Task<IResult> DeleteCategory(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteCategoryCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.NoContent();
        }
    }
}
