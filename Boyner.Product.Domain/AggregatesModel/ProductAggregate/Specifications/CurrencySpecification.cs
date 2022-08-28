using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate.Specifications
{
    public class CurrencySpecification : Specification<Currency>, ISingleResultSpecification
    {
        public CurrencySpecification(string currencyCode)
        {
            Query.Where(x => x.DeletedOn == null && x.CurrencyCode == currencyCode);
        }
    }
}
