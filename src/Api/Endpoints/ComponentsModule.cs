using Application.Common.Models;
using Application.Components.Commands.CreateComponent;
using Application.Components.Commands.DeleteComponent;
using Application.Components.Commands.UpdateComponent;
using Application.Components.Queries.GetComponents;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class ComponentsModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/components");

            builder.MapGet("", GetComponents);
            builder.MapPost("", CreateComponent);
            builder.MapPut("{id:int}", UpdateComponent);
            builder.MapDelete("{id:int}", DeleteComponent);
        }

        public async Task<IResult> GetComponents(ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetComponentsQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateComponent(ISender sender, CreateComponentCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateComponent(ISender sender, int id, UpdateComponentCommand command)
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

        public async Task<IResult> DeleteComponent(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteComponentCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.NoContent();
        }
    }
}
