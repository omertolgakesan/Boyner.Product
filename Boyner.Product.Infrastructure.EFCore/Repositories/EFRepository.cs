using Ardalis.Specification.EntityFrameworkCore;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    #pragma warning disable
    public class EFRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly BoynerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public EFRepository(BoynerContext context) : base(context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public override async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Add(entity);

            return await Task.FromResult<T>(entity);
        }
        public override async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(_context.Entry(entity).State = EntityState.Modified);
        }
        public override async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(_context.Set<T>().Remove(entity));
        }
        
        public override async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public override async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new Exception("Dont use this SaveChangesAsync() It's forbidden!");
        }

        /*
         *  Add additional functionalities if required.
         */

    }
}
