using Boyner.Product.Domain.SharedKernel.SeedWork;
using MediatR;

namespace Boyner.Product.Domain.Events
{
    public class ProductDeletedDomainEvent : DomainEvent, INotification
    {
        public AggregatesModel.ProductAggregate.Product Product { get; private set; }
        public ProductDeletedDomainEvent(AggregatesModel.ProductAggregate.Product product)
        {
            Product = product;
        }
    }
}
