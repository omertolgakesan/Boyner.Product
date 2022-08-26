using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boyner.Product.Infrastructure.EFCore.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> categoryBuilder)
        {
            categoryBuilder.ToTable("Category", BoynerContext.DEFAULT_SCHEMA);

            categoryBuilder.HasKey(c => c.Id); //PK

            categoryBuilder.Property(c => c.Id).HasColumnType("UUID").ValueGeneratedNever().IsRequired();

            categoryBuilder.Property(c => c.Name).HasColumnType("character varying").IsRequired().HasMaxLength(250);

            categoryBuilder.Property(c => c.StatusId).HasColumnType("smallint");
            categoryBuilder.HasOne(c => c.CategoryStatus).WithMany().HasForeignKey(fk => fk.StatusId);

            categoryBuilder.Property(c => c.CreatedOn).IsRequired();

            categoryBuilder.Property(c => c.UpdatedOn);

            categoryBuilder.Property(c => c.DeletedOn);

            /* Collection */
            categoryBuilder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(fk => fk.CategoryId);

            /* Ignore */
            categoryBuilder.Ignore(c => c.DomainEvents);
        }
    }
}
