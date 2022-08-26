using Boyner.Product.Domain.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.CategoryAggregate
{
    public class CategoryStatus : Enumeration
    {
        public static CategoryStatus Active = new CategoryStatus(1, nameof(Active));
        public static CategoryStatus Passive = new CategoryStatus(2, nameof(Passive));

        public CategoryStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<CategoryStatus> List() =>
            new[]
            {
                Active,
                Passive,
            };

        public static CategoryStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new DomainException($"Possible values for CategoryStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static CategoryStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new DomainException($"Possible values for CategoryStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
