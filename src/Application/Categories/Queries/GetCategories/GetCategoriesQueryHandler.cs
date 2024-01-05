using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Categories.Queries.GetCategories
{
    internal sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, PagedList<CategoryResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetCategoriesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Category> categoriesQuery = _context.Categories
                .Include(p => p.CategoryType);

            if (!string.IsNullOrWhiteSpace(request.LoadOptions.SearchTerm))
            {
                categoriesQuery = categoriesQuery.Where(p =>
                    p.Name.Contains(request.LoadOptions.SearchTerm) ||
                    p.CategoryType.Name.Contains(request.LoadOptions.SearchTerm) ||
                    p.Quantity.ToString().Contains(request.LoadOptions.SearchTerm));
            }

            if (request.LoadOptions.SortOrder?.ToLower() == "desc")
            {
                categoriesQuery = categoriesQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                categoriesQuery = categoriesQuery.OrderBy(GetSortProperty(request));
            }

            var categories = categoriesQuery
                .Select(p =>
                    new CategoryResponse(
                        p.Id,
                        p.Name,
                        p.CategoryType.Name,
                        p.SendEmailNotification,
                        p.Quantity));

            var pagedCategories = await PagedList<CategoryResponse>.CreateAsync(categories, request.LoadOptions.PageNumber, request.LoadOptions.PageSize);

            return pagedCategories;
        }

        private static Expression<Func<Category, object>> GetSortProperty(GetCategoriesQuery request) => request.LoadOptions.SortColumn?.ToLower() switch
        {
            "name" => category => category.Name,
            "type" => category => category.CategoryType.Name,
            "quantity" => category => category.Quantity,
            _ => category => category.Id
        };
    }
}
