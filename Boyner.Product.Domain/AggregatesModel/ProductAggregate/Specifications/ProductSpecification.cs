using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate.Specifications
{
    public class ProductSpecification : Specification<Product>, ISingleResultSpecification
    {
        public ProductSpecification(Guid id)
        {
            Query.Where(p => p.Id == id)
                 .Where(p => p.StatusId == ProductStatus.Active.Id)
                 .OrderBy(x => x.Name);
        }

        public ProductSpecification(string name)
        {
            Query.Where(p => p.Name == name);
            Query.Where(p => p.StatusId == ProductStatus.Active.Id);
        }

        public ProductSpecification(string? name, decimal? minimumPrice, decimal? maximumPrice, string? categoryName)
        {
            if (name != null)
                Query.Where(p => p.Name == name);
            if (minimumPrice != null)
                Query.Where(p => p.Price >= minimumPrice.Value);
            if (maximumPrice != null)
                Query.Where(p => p.Price <= maximumPrice.Value);
            if (categoryName != null)
                Query.Include(x => x.Category).Where(p => p.Category.Name == categoryName);
            Query.Where(p => p.StatusId == ProductStatus.Active.Id);
            Query.Include(x => x.Category).Include(x => x.Currency).Include(x => x.ProductAttributes).ThenInclude(x => x.AttributeValue).ThenInclude(x=>x.Attribute);
        }
    }
}
