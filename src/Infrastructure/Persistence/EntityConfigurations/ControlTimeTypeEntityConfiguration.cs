using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal sealed class ControlTimeTypeEntityConfiguration : IEntityTypeConfiguration<ControlTimeType>
    {
        public void Configure(EntityTypeBuilder<ControlTimeType> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Name)
                .HasMaxLength(200);
        }
    }
}
