using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Domain.SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Domain.AggregatesModel.ProductAggregate
{
    public class Currency : Entity, IAggregateRoot
    {

        public string Name { get; private set; }
        public string CurrencyCode { get; private set; }

        protected Currency() { }

        public Currency(Guid id, string name, string currencyCode)
        {
            Check.HasValue(id, nameof(id));
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNullOrEmpty(currencyCode, nameof(currencyCode));
            Id = id;
            Name = name;
            CurrencyCode = currencyCode;
            CreatedOn = DateTime.Now;
        }
    }
}
