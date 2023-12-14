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
        DbSet<Employee> Employees { get; }
        DbSet<License> Licenses { get; }
        DbSet<Manufacturer> Manufacturers { get; }

        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
