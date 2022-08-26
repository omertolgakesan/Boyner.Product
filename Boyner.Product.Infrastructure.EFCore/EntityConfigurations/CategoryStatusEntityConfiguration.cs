using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boyner.Product.Infrastructure.EFCore.EntityConfigurations
{
    class CategoryStatusEntityConfiguration : IEntityTypeConfiguration<CategoryStatus>
    {
        public void Configure(EntityTypeBuilder<CategoryStatus> categoryStatusBuilder)
        {
            categoryStatusBuilder.ToTable("CategoryStatus", BoynerContext.DEFAULT_SCHEMA);

            categoryStatusBuilder.HasKey(o => o.Id);

            categoryStatusBuilder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            categoryStatusBuilder.Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            /* Seed Data */
            //categoryStatusBuilder.HasData(CategoryStatus.List());
        }
    }
}
