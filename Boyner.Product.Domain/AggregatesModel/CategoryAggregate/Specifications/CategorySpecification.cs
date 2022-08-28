using Ardalis.Specification;
using System;
using System.Linq;

namespace Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications
{
    public class CategorySpecification : Specification<Category>, ISingleResultSpecification
    {
        public CategorySpecification(Guid id,bool includeRelations = false)
        {
            Query.Where(p => p.Id == id && p.DeletedOn == null);

            if (includeRelations)
            {
                Query.Include(x => x.Products).Include(x => x.CategoryAtrributes).ThenInclude(x => x.Attribute).Include(x => x.CategoryAtrributes).ThenInclude(x => x.Attribute.AttributeValues);
            }
        }

        public CategorySpecification()
        {
            Query.Include(x => x.Products).Include(x => x.CategoryAtrributes).ThenInclude(x => x.Attribute).Include(x=>x.CategoryAtrributes).ThenInclude(x=>x.Attribute.AttributeValues);
        }
    }
}
