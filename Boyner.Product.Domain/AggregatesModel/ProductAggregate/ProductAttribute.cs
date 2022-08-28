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

        public AttributeValue AttributeValue { get; private set; }
        public Guid AttributeValueId { get; set; }

        protected ProductAttribute() { }


        public ProductAttribute(Guid id,Guid productId, Guid attributeValueId)
        {
            Check.HasValue(id, nameof(id));
            Check.HasValue(productId, nameof(productId));
            Check.HasValue(attributeValueId, nameof(attributeValueId));

            this.Id = id;
            this.ProductId = productId;
            this.AttributeValueId = attributeValueId;
            this.CreatedOn = DateTime.Now;
        }
    }
}
