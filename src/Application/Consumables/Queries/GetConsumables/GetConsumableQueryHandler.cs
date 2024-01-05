using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Consumables.Queries.GetConsumables
{
    internal sealed class GetConsumableQueryHandler : IQueryHandler<GetConsumablesQuery, PagedList<ConsumableResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetConsumableQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PagedList<ConsumableResponse>>> Handle(GetConsumablesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Consumable> consumablesQuery = _context.Consumables
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Department);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                consumablesQuery = consumablesQuery.Where(p =>
                    p.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (!string.IsNullOrWhiteSpace(p.ModelNo) && p.ModelNo.Contains(request.LoadOptions.SearchTerm)) ||
                    p.Category.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (p.Manufacturer != null && p.Manufacturer.Name.Contains(request.LoadOptions.SearchTerm)) ||
                    (p.Department != null && p.Department.Name.Contains(request.LoadOptions.SearchTerm)));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                consumablesQuery = consumablesQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                consumablesQuery = consumablesQuery.OrderBy(GetSortProperty(request));
            }

            var consumables = consumablesQuery
                .Select(p => new ConsumableResponse(
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

            return await PagedList<ConsumableResponse>
                .CreateAsync(consumables, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);
        }

        private static Expression<Func<Consumable, object>> GetSortProperty(GetConsumablesQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "name" => component => component.Name,
            "category" => component => component.Category.Name,
            "purchase_date" => component => component.PurchaseDate,
            "purchase_cost" => component => component.PurchaseCost,
            _ => component => component.Id
        };
    }
}
