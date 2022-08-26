using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.SharedKernel.SeedWork;
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
    }
}
