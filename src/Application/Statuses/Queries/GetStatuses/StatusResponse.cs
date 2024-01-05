namespace Application.Statuses.Queries.GetStatuses
{
    public sealed record StatusResponse(int Id, string Name, int TypeId, string TypeName, bool IsSystem, int AssetsCount);
}
