using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries.GetCategories;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class CategoriesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var categories = app.MapGroup("api/categories");

            categories.MapGet("/", async (
                ISender sender,
                string? searchTerm,
                string? sortColumn,
                string? sortOrder,
                int pageNumber = 1,
                int pageSize = 10) =>
            {
                return Results.Ok(await sender.Send(new GetCategoriesQuery(searchTerm, sortColumn, sortOrder, pageNumber, pageSize)));
            });

            categories.MapGet("/{id:int}", (int id, ISender sender) =>
            {
                return Results.Ok();
            });

            categories.MapPost("/", async (CreateCategoryCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            });

            categories.MapPut("/{id:int}", async (int id, UpdateCategoryCommand command, ISender sender) =>
            {
                if (id != command.Id)
                {
                    return Results.BadRequest();
                }

                var result = await sender.Send(command);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.NoContent();
            });

            categories.MapDelete("/{id:int}", async (int id, ISender sender) =>
            {
                var command = new DeleteCategoryCommand(id);
                var result = await sender.Send(command);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.NoContent();
            });
        }
    }
}
