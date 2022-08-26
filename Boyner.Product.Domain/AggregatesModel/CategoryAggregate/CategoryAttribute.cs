using Boyner.Product.Domain.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.CategoryAggregate
{
    public class CategoryAttribute : Entity
    {
        public Category Category { get; private set; }
        public Guid CategoryId { get; private set; }
        public AttributeAggregate.Attribute Attribute { get; private set; }
        public Guid AttributeId { get; private set; }


        public CategoryAttribute(Guid categoryId, Guid attributeId)
        {
            this.CategoryId = categoryId;
            this.AttributeId = attributeId;
            this.CreatedOn = DateTime.Now;
        }

    }
}
