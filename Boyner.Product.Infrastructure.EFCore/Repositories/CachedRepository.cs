using Ardalis.Specification;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    /// <inheritdoc/>
    public class CachedRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachedRepository<T>> _logger;
        private readonly EFRepository<T> _sourceRepository;
        private MemoryCacheEntryOptions _cacheOptions;
        public IUnitOfWork UnitOfWork => _sourceRepository.UnitOfWork;

        public CachedRepository(IMemoryCache cache,
            ILogger<CachedRepository<T>> logger,
            EFRepository<T> sourceRepository)
        {
            _cache = cache;
            _logger = logger;
            _sourceRepository = sourceRepository;

            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(relative: TimeSpan.FromSeconds(10));
        }

        /// <inheritdoc/>
        public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            // TODO: Add Caching
            return _sourceRepository.CountAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _sourceRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            return _sourceRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
        {
            if (specification.CacheEnabled)
            {
                string key = $"{specification.CacheKey}-GetBySpecAsync";
                _logger.LogInformation("Checking cache for " + key);
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning("Fetching source data for " + key);
                    return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            string key = $"{nameof(T)}-ListAsync";
            return _cache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(_cacheOptions);
                return _sourceRepository.ListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            if (specification.CacheEnabled)
            {
                string key = $"{specification.CacheKey}-ListAsync";
                _logger.LogInformation("Checking cache for " + key);
                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogWarning("Fetching source data for " + key);
                    return _sourceRepository.ListAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.ListAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
