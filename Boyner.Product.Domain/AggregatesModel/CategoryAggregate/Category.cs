using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.CategoryAggregate
{
    public class Category : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public CategoryStatus CategoryStatus { get; private set; }
        public int StatusId { get; private set; }
        public virtual ICollection<ProductAggregate.Product> Products { get; private set; }
        public virtual ICollection<CategoryAttribute> CategoryAtrributes { get; private set; }

        public Category(Guid id, string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            this.Id = id;
            this.Name = name;

            this.StatusId = CategoryStatus.Active.Id;
            this.CreatedOn = DateTime.Now;

            this.CategoryAtrributes = new HashSet<CategoryAttribute>();
            this.Products = new HashSet<ProductAggregate.Product>();

        }

        public void AddCategoryAttribute(CategoryAttribute categoryAttribute)
        {
            this.CategoryAtrributes.Add(categoryAttribute);
        }

    }
}
