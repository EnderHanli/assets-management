namespace Application.Common.Models
{
    public sealed record LoadOptions(string? SearchTerm, string? SortColumn, string? SortOrder, int PageNumber = 1, int PageSize = 10);

}
