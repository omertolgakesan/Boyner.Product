using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate
{
    public class ProductAttribute : Entity
    {
        public Product Product { get; private set; }
        public Guid ProductId { get; private set; }
        public AttributeAggregate.Attribute Attribute { get; private set; }
        public Guid AttributeId { get; private set; }
        public AttributeValue AttributeValue { get; private set; }
        public Guid AttributeValueId { get; set; }

        protected ProductAttribute(){}


        public ProductAttribute(Product product, AttributeAggregate.Attribute attribute, AttributeValue attributeValue)
        {
            Check.NotNull(product, nameof(product));
            Check.NotNull(attribute, nameof(attribute));
            Check.NotNull(attributeValue, nameof(attributeValue));

            this.ProductId = product.Id;
            this.AttributeId = attribute.Id;
            this.AttributeValueId = attributeValue.Id;
            this.CreatedOn = DateTime.Now;
        }
    }
}
