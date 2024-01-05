using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Components.Queries.GetComponents
{
    internal sealed class GetComponentQueryHandler : IQueryHandler<GetComponentsQuery, PagedList<ComponentResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetComponentQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PagedList<ComponentResponse>>> Handle(GetComponentsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Component> componentsQuery = _context.Components
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Department);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                componentsQuery = componentsQuery.Where(p =>
                    p.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (!string.IsNullOrWhiteSpace(p.ModelNo) && p.ModelNo.Contains(request.LoadOptions.SearchTerm)) ||
                    p.Category.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (p.Manufacturer != null && p.Manufacturer.Name.Contains(request.LoadOptions.SearchTerm)) ||
                    (p.Department != null && p.Department.Name.Contains(request.LoadOptions.SearchTerm)));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                componentsQuery = componentsQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                componentsQuery = componentsQuery.OrderBy(GetSortProperty(request));
            }

            var components = componentsQuery
                .Select(p => new ComponentResponse(
                    p.Id,
                    p.Name,
                    p.Quantity,
                    p.ModelNo,
                    p.PurchaseDate,
                    p.PurchaseCost,
                    p.Category,
                    p.Manufacturer,
                    p.Department,
                    p.Notes));

            return await PagedList<ComponentResponse>
                .CreateAsync(components, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);
        }

        private static Expression<Func<Component, object>> GetSortProperty(GetComponentsQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "name" => component => component.Name,
            "category" => component => component.Category.Name,
            "purchase_date" => component => component.PurchaseDate,
            "purchase_cost" => component => component.PurchaseCost,
            _ => component => component.Id
        };
    }
}
