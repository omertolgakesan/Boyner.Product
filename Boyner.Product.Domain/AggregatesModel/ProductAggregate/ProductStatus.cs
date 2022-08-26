using Boyner.Product.Domain.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate
{
    public class ProductStatus : Enumeration
    {
        public static ProductStatus Active = new ProductStatus(1, nameof(Active));
        public static ProductStatus Passive = new ProductStatus(2, nameof(Passive));

        public ProductStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<ProductStatus> List() =>
            new[]
            {
                Active,
                Passive
            };

        public static ProductStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new DomainException($"Possible values for ProductStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ProductStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new DomainException($"Possible values for ProductStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
