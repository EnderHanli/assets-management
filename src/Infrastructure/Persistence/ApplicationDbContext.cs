using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Accessory> Accessories => Set<Accessory>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Component> Components => Set<Component>();
        public DbSet<Consumable> Consumables => Set<Consumable>();
        public DbSet<License> Licenses => Set<License>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<CategoryType> CategoryTypes => Set<CategoryType>();
        public DbSet<StatusType> StatusTypes => Set<StatusType>();
        public DbSet<ControlTimeType> ControlTimeTypes => Set<ControlTimeType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
