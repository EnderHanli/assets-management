using Application.Assets.Commands.CreateAsset;
using Application.Assets.Commands.DeleteAsset;
using Application.Assets.Commands.UpdateAsset;
using Application.Assets.Queries.GetAssets;
using Application.Common.Models;
using Carter;
using MediatR;

namespace Api.Endpoints
{
    public class AssetsModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var builder = app.MapGroup("api/assets");

            builder.MapGet("", GetAssets);
            builder.MapPost("", CreateAsset);
            builder.MapPut("{id:int}", UpdateAsset);
            builder.MapDelete("{id:int}", DeleteAsset);
        }

        public async Task<IResult> GetAssets(ISender sender, [AsParameters] LoadOptions loadOptions)
        {
            var result = await sender.Send(new GetAssetsQuery(loadOptions));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> CreateAsset(ISender sender, CreateAssetCommand command)
        {
            var result = await sender.Send(command);
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Ok(result.Value);
        }

        public async Task<IResult> UpdateAsset(ISender sender, int id, UpdateAssetCommand command)
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

        public async Task<IResult> DeleteAsset(ISender sender, int id)
        {
            var result = await sender.Send(new DeleteAssetCommand(id));
            if (!result.Succeeded)
            {
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.NoContent();
        }
    }
}
