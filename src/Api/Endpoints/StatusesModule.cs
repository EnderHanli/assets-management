using Application.Common.Models;
using Application.Statuses.Commands.CreateStatus;
using Application.Statuses.Commands.DeleteStatus;
using Application.Statuses.Commands.UpdateStatus;
using Application.Statuses.Queries.GetStatuses;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class StatusesModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/statuses/");

            builder.MapGet("", GetStatuses);
            builder.MapPost("", CreateStatus);
            builder.MapPut("{id:int}", UpdateStatus);
            builder.MapDelete("{id:int}", DeleteStatus);
        }

        public async Task<IResult> GetStatuses(ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetStatusesQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateStatus(ISender sender, CreateStatusCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateStatus(ISender sender, int id, UpdateStatusCommand command)
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

        public async Task<IResult> DeleteStatus(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteStatusCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok();
        }
    }
}
