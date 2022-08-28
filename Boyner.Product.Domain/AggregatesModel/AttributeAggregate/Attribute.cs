using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.AttributeAggregate
{
    public class Attribute : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public virtual ICollection<AttributeValue> AttributeValues { get; private set; }

        protected Attribute(){}

        public Attribute(Guid id, string name)
        {
            Check.HasValue(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));

            this.Id = id;
            this.Name = name;
            this.CreatedOn = DateTime.Now;

            this.AttributeValues = new List<AttributeValue>();
        }

        public void AddAttributeValues(List<string> attributeValues)
        {
            foreach (var attributeValue in attributeValues)
            {
                var attribute = new AttributeValue(Guid.NewGuid(), attributeValue, this);
                AttributeValues.Add(attribute);
            }
        }

        public void DeleteAttribute()
        {
            this.DeletedOn = DateTime.Now;

            foreach (var attributeValue in this.AttributeValues)
            {
                attributeValue.DeleteAttributeValue();
            }
        }

        public void UpdateName(string name)
        {
            Check.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
            this.UpdatedOn = DateTime.Now;
        }


    }
}
