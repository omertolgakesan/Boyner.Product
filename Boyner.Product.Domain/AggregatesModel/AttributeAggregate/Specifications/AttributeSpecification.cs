using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.AttributeAggregate.Specifications
{
    public class AttributeSpecification : Specification<Attribute>, ISingleResultSpecification
    {
        public AttributeSpecification()
        {
            Query.Where(x => x.DeletedOn == null);
            Query.Include(x => x.AttributeValues);
        }

        public AttributeSpecification(Guid id)
        {
            Query.Where(x => x.Id == id);
            Query.Where(x => x.DeletedOn == null);
            Query.Include(x => x.AttributeValues);
        }

        public AttributeSpecification(List<Guid> ids)
        {
            Query.Where(x => ids.Contains(x.Id));
            Query.Where(x => x.DeletedOn == null);
        }
    }
}
