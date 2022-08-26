using System;

namespace Boyner.Product.Domain.SharedKernel.SeedWork
{
    public abstract class DomainEvent
    {
        public string Type => this.GetType().Name;
        public readonly Guid Id;
        public readonly Guid CorrelationID;
        public readonly DateTime CreatedOn;

        public DomainEvent()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }
        public DomainEvent(Guid correlationID)
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            this.CorrelationID = correlationID;
        }
    }
}
