using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Accesories.Queries.GetAccessories
{
    internal sealed class GetAccessoriesQueryHandler : IQueryHandler<GetAccessoriesQuery, PagedList<AccessoryResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAccessoriesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PagedList<AccessoryResponse>>> Handle(GetAccessoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Accessory> accesoriesQuery = _context.Accessories
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Department);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                accesoriesQuery = accesoriesQuery.Where(p =>
                    p.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (!string.IsNullOrWhiteSpace(p.ModelNo) && p.ModelNo.Contains(request.LoadOptions.SearchTerm)) ||
                    p.Category.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (p.Manufacturer != null && p.Manufacturer.Name.Contains(request.LoadOptions.SearchTerm)) ||
                    (p.Department != null && p.Department.Name.Contains(request.LoadOptions.SearchTerm)));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                accesoriesQuery = accesoriesQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                accesoriesQuery = accesoriesQuery.OrderBy(GetSortProperty(request));
            }

            var accessories = accesoriesQuery
                .Select(p => new AccessoryResponse(
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

            return await PagedList<AccessoryResponse>
                .CreateAsync(accessories, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);
        }

        private static Expression<Func<Accessory, object>> GetSortProperty(GetAccessoriesQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "name" => component => component.Name,
            "category" => component => component.Category.Name,
            "purchase_date" => component => component.PurchaseDate,
            "purchase_cost" => component => component.PurchaseCost,
            _ => component => component.Id
        };
    }
}
