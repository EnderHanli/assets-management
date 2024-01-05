using Application.Accesories.Commands.CreateAccessory;
using Application.Accesories.Commands.DeleteAccessory;
using Application.Accesories.Commands.UpdateAccessory;
using Application.Accesories.Queries.GetAccessories;
using Application.Common.Models;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class AccessoriesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/accessories");

            builder.MapGet("", GetAccesories);
            builder.MapPost("", CreateAccessory);
            builder.MapPut("{id:int}", UpdateAccessory);
            builder.MapDelete("{id:int}", DeleteAccessory);
        }

        public async Task<IResult> GetAccesories(ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetAccessoriesQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateAccessory(ISender sender, CreateAccessoryCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateAccessory(ISender sender, int id, UpdateAccessoryCommand command)
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

        public async Task<IResult> DeleteAccessory(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteAccessoryCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.NoContent();
        }
    }
}
