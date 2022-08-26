using Boyner.Product.Domain.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.AttributeAggregate
{
    public class AttributeValue : Entity
    {
        public string Name { get; private set; }
        public Guid AttributeId { get; private set; }
        public Attribute Attribute { get; private set; }
    }
}
