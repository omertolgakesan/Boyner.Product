using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boyner.Product.Infrastructure.EFCore.EntityConfigurations
{
    class ProductStatusEntityConfiguration : IEntityTypeConfiguration<ProductStatus>
    {
        public void Configure(EntityTypeBuilder<ProductStatus> productStatusBuilder)
        {
            productStatusBuilder.ToTable("ProductStatus", BoynerContext.DEFAULT_SCHEMA);

            productStatusBuilder.HasKey(o => o.Id);

            productStatusBuilder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            productStatusBuilder.Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            /* Seed Data */
            //productStatusBuilder.HasData(CategoryStatus.List());
        }
    }
}
