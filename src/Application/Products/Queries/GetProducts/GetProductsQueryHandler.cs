using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Products.Queries.GetProducts
{
    internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PagedList<ProductsResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PagedList<ProductsResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(request.LoadOptions.SearchTerm) ||
                    p.Category.Name.Contains(request.LoadOptions.SearchTerm) ||
                    (!string.IsNullOrWhiteSpace(p.ModelNo) && p.ModelNo.Contains(request.LoadOptions.SearchTerm)));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                productsQuery = productsQuery.OrderBy(GetSortProperty(request));
            }

            var products = productsQuery
                .Select(p =>
                    new ProductsResponse(
                        p.Id,
                        p.Name,
                        p.ModelNo,
                        p.Category,
                        p.Manufacturer,
                        p.PurchaseCost,
                        p.Notes,
                        p.IsArchived,
                        p.Assets.Count
                ));

            var pagedProducts = await PagedList<ProductsResponse>.CreateAsync(products, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);

            return pagedProducts;
        }

        private static Expression<Func<Product, object>> GetSortProperty(GetProductsQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            _ => product => product.Id
        };
    }
}
