using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Products.Queries.GetProducts
{
    internal sealed class GetProductQueryHandler : IQueryHandler<GetProductsQuery, List<ProductsResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Result<List<ProductsResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(request.SearchTerm) ||
                    p.Category.Name.Contains(request.SearchTerm) ||
                    p.ModelNo.Contains(request.SearchTerm));
            }

            if (request.SortOrder?.ToLower() == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                productsQuery = productsQuery.OrderBy(GetSortProperty(request));
            }

            var products = await productsQuery
                .Select(p =>
                    new ProductsResponse(
                        p.Id,
                        p.Name,
                        new CategoryResponse(p.Category.Id, p.Category.Name),
                        p.ModelNo,
                        new ManufacturerResponse(p.Manufacturer.Id, p.Manufacturer.Name),
                        p.PurchaseCost,
                        p.Notes,
                        p.IsArchived,
                        p.Assets.Count
                ).ToListAsync(cancellationToken);

            return Result.Sproducts;
        }

        private static Expression<Func<Product, object>> GetSortProperty(GetProductsQuery request) => request.SortColumn?.ToLower() switch
        {
            "name" => category => category.Name,
            _ => category => category.Id
        };
    }
}
