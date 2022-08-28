using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boyner.Product.Infrastructure.EFCore.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.ProductAggregate.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.ProductAggregate.Product> productBuilder)
        {
            productBuilder.ToTable("Product", BoynerContext.DEFAULT_SCHEMA);

            productBuilder.HasKey(p => p.Id); //PK

            productBuilder.Property(p => p.Id).HasColumnType("uniqueidentifier").ValueGeneratedNever().IsRequired();

            productBuilder.Property(p => p.Name).HasColumnType("character varying").IsRequired().HasMaxLength(250);


            productBuilder.Property(p => p.CategoryId);
            productBuilder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(fk => fk.CategoryId);

            productBuilder.Property(p => p.Price).HasColumnType("money");

            productBuilder.Property(p => p.StatusId).HasColumnType("int");
            productBuilder.HasOne(c => c.Status).WithMany().HasForeignKey(fk => fk.StatusId);

            productBuilder.Property(p => p.CreatedOn).IsRequired();

            productBuilder.Property(p => p.UpdatedOn);

            productBuilder.Property(p => p.DeletedOn);

            /* Ignore */
            productBuilder.Ignore(p => p.DomainEvents);
        }
    }
}
