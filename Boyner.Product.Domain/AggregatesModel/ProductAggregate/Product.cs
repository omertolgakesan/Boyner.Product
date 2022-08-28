using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.Events;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public int StatusId { get; private set; }
        public ProductStatus Status { get; private set; }
        public Currency Currency { get; private set; }
        public Guid CurrencyId { get; private set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; private set; }

        protected Product() { }

        public Product(Guid id, string name, decimal price, Guid categoryId, Guid currencyId)
        {
            Check.HasValue(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));
            Check.HasValue(categoryId, nameof(categoryId));
            Check.HasValue(currencyId, nameof(currencyId));
            Check.Positive<decimal>(price, nameof(price));

            this.Id = id;
            this.Name = name;
            this.CategoryId = categoryId;
            this.Price = price;
            this.StatusId = ProductStatus.Active.Id;
            this.CurrencyId = currencyId;

            this.CreatedOn = DateTime.Now;

            this.ProductAttributes = new HashSet<ProductAttribute>();
        }

        public void AddAttributeValue(AttributeValue attributeValue)
        {
            this.ProductAttributes.Add(new ProductAttribute(Guid.NewGuid(), this.Id, attributeValue.Id));
        }

        public void Update(string name, Category category, decimal price)
        {
            Check.NotNull(category, nameof(category));
            Check.Positive(price, nameof(price));
            Check.NotNullOrEmpty(name, nameof(name));

            this.Price = price;
            this.Name = name.Trim();
            this.CategoryId = category.Id;
        }

        public void UpdatePrice(decimal price)
        {
            Check.Positive(price, nameof(price));

            this.Price = price;
            this.UpdatedOn = DateTime.Now;
        }

        public void Delete()
        {
            this.StatusId = ProductStatus.Passive.Id;
            this.UpdatedOn = DateTime.Now;
            this.DeletedOn = DateTime.Now;

            AddDomainEvent(new ProductDeletedDomainEvent(this));

        }
    }
}
