using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Categories.Queries.GetCategories
{
    internal sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<CategoryResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetCategoriesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Category> categoriesQuery = _context.Categories;

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                categoriesQuery = categoriesQuery.Where(p =>
                    p.Name.Contains(request.SearchTerm) ||
                    p.CategoryType.ToString().Contains(request.SearchTerm) ||
                    p.Quantity.ToString().Contains(request.SearchTerm));
            }

            if (request.SortOrder?.ToLower() == "desc")
            {
                categoriesQuery = categoriesQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                categoriesQuery = categoriesQuery.OrderBy(GetSortProperty(request));
            }

            var categories = await categoriesQuery
                .Select(p =>
                    new CategoryResponse(
                        p.Id,
                        p.Name,
                        p.CategoryType,
                        p.SendEmailNotification,
                        p.Quantity)
                ).ToListAsync(cancellationToken);

            return categories;
        }

        private static Expression<Func<Category, object>> GetSortProperty(GetCategoriesQuery request) => request.SortColumn?.ToLower() switch
        {
            "name" => category => category.Name,
            "type" => category => (int)category.CategoryType,
            "quantity" => category => category.Quantity,
            _ => category => category.Id
        };
    }
}
