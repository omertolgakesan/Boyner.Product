using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.AttributeAggregate
{
    public class AttributeValue : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public Guid AttributeId { get; set; }
        public Attribute Attribute { get; set; }

        protected AttributeValue() { }

        public AttributeValue(Guid id, string name, Guid attributeId)
        {
            Check.HasValue(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));
            Check.HasValue(attributeId, nameof(attributeId));

            this.Id = id;
            this.Name = name;
            this.AttributeId = attributeId;
        }

        internal void DeleteAttributeValue()
        {
            this.DeletedOn = DateTime.Now;
        }
    }
}
