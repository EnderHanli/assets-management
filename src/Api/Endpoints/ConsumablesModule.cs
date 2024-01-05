using Application.Common.Models;
using Application.Consumables.Commands.CreateConsumable;
using Application.Consumables.Commands.DeleteConsumable;
using Application.Consumables.Commands.UpdateConsumable;
using Application.Consumables.Queries.GetConsumables;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class ConsumablesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/consumables");

            builder.MapGet("", GetConsumables);
            builder.MapPost("", CreateConsumable);
            builder.MapPut("{id:int}", UpdateConsumable);
            builder.MapDelete("{id:int}", DeleteConsumable);
        }

        public async Task<IResult> GetConsumables(ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetConsumablesQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateConsumable(ISender sender, CreateConsumableCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateConsumable(ISender sender, int id, UpdateConsumableCommand command)
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

            return TypedResults.Ok();
        }

        public async Task<IResult> DeleteConsumable(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteConsumableCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok();
        }
    }
}
