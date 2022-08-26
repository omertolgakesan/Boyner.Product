using Ardalis.Specification;

namespace Boyner.Product.Domain.SharedKernel.SeedWork
{
    /// <inheritdoc/>
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }

    /// <inheritdoc/>
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {

    }
}
