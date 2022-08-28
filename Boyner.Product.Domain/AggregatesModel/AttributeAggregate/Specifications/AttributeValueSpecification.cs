using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.AttributeAggregate.Specifications
{
    public class AttributeValueSpecification : Specification<AttributeValue>, ISingleResultSpecification
    {
        public AttributeValueSpecification()
        {
            Query.Where(x => x.DeletedOn == null);
            Query.Include(x => x.Attribute);
        }

        public AttributeValueSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
            Query.Where(x => x.DeletedOn == null);
            Query.Include(x => x.Attribute);
        }

        public AttributeValueSpecification(List<Guid> ids)
        {
            Query.Where(x => ids.Contains(x.Id));
            Query.Where(x => x.DeletedOn == null);
            Query.Include(x => x.Attribute);
        }
    }
}
