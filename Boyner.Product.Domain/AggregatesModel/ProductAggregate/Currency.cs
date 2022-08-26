using Boyner.Product.Domain.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate
{
    public class Currency: Entity
    {
        public string Name { get; private set; }
        public string CurrencyCode { get; private set; }

    }
}
