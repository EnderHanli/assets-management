using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Api.Infrastructure
{
    public static class ApplicationContextSeed
    {
        public static async Task SeedAsync(this ApplicationDbContext context)
        {
            if (!context.CategoryTypes.Any())
            {
                context.CategoryTypes.AddRange(GetPredefineCategoryTypes());
            }

            if (!context.StatusTypes.Any())
            {
                context.StatusTypes.AddRange(GetPredefineStatusType());
            }

            if (!context.ControlTimeTypes.Any())
            {
                context.ControlTimeTypes.AddRange(GetPredefineControlTimeType());
            }

            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(
                    new List<Status>()
                    {
                        new() { Name= "Archived", StatusTypeId = StatusType.Archived.Id, IsSystem = true },
                        new() { Name= "Ready to Deploy", StatusTypeId = StatusType.Deployable.Id, IsSystem = true }
                    });
            }

            await context.SaveChangesAsync();
        }

        private static IEnumerable<CategoryType> GetPredefineCategoryTypes()
        {
            return Enumeration.GetAll<CategoryType>();
        }

        private static IEnumerable<StatusType> GetPredefineStatusType()
        {
            return Enumeration.GetAll<StatusType>();
        }

        private static IEnumerable<ControlTimeType> GetPredefineControlTimeType()
        {
            return Enumeration.GetAll<ControlTimeType>();
        }
    }
}
