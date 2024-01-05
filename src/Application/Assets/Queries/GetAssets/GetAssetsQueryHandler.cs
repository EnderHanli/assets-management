using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Assets.Queries.GetAssets
{
    internal sealed class GetAssetsQueryHandler : IQueryHandler<GetAssetsQuery, PagedList<AssetResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAssetsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PagedList<AssetResponse>>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Asset> assetsQuery = _context.Assets
                .Include(p => p.Product)
                .Include(p => p.Product.Category)
                .Include(p => p.Product.Manufacturer)
                .Include(p => p.Department)
                .Include(p => p.Status);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                assetsQuery = assetsQuery.Where(p =>
                    p.Code.Contains(request.LoadOptions.SearchTerm) ||
                    (!string.IsNullOrWhiteSpace(p.Name) && p.Name.Contains(request.LoadOptions.SearchTerm)) ||
                    (!string.IsNullOrWhiteSpace(p.SerialNumber) && p.SerialNumber.Contains(request.LoadOptions.SearchTerm)) ||
                    p.Product.Name.Contains(request.LoadOptions.SearchTerm) ||
                    p.Product.Category.Name.Contains(request.LoadOptions.SearchTerm) ||
                    p.Status.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (p.Department != null && p.Department.Name.Contains(request.LoadOptions.SearchTerm)) ||
                    (p.Product.Manufacturer != null && p.Product.Manufacturer.Name.Contains(request.LoadOptions.SearchTerm)));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                assetsQuery = assetsQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                assetsQuery = assetsQuery.OrderBy(GetSortProperty(request));
            }

            var assets = assetsQuery
                .Select(p => new AssetResponse(
                    p.Id,
                    p.Code,
                    p.Product.Name,
                    p.Status.Name,
                    p.Name,
                    p.SerialNumber,
                    p.WarrantyExpirationDate,
                    p.PurchaseDate,
                    p.PurchaseCost,
                    p.Product.Category.Name,
                    p.Product.Manufacturer.Name,
                    p.Department.Name,
                    p.Notes));

            var pagedAssets = await PagedList<AssetResponse>.CreateAsync(assets, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);

            return pagedAssets;
        }

        private static Expression<Func<Asset, object>> GetSortProperty(GetAssetsQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "code" => product => product.Code,
            "name" => product => product.Name,
            "purchase_date" => product => product.PurchaseDate,
            "purchase_cost" => product => product.PurchaseCost,
            _ => category => category.Id
        };
    }
}
