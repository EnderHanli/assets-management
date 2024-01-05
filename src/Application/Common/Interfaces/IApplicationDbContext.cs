using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Status> Statuses { get; }
        DbSet<Product> Products { get; }
        DbSet<Accessory> Accessories { get; }
        DbSet<Asset> Assets { get; }
        DbSet<Component> Components { get; }
        DbSet<Consumable> Consumables { get; }
        DbSet<License> Licenses { get; }
        DbSet<Employee> Employees { get; }
        DbSet<Department> Departments { get; }
        DbSet<Manufacturer> Manufacturers { get; }
        DbSet<CategoryType> CategoryTypes { get; }
        DbSet<StatusType> StatusTypes { get; }
        DbSet<ControlTimeType> ControlTimeTypes { get; }

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
