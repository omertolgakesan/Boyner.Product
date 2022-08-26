namespace Boyner.Product.Domain.SharedKernel.SeedWork
{
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
    public interface IEntityBase
    { }
}
