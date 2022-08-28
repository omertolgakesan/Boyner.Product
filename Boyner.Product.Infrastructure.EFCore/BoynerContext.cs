using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Infrastructure.EFCore.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore
{
    public class BoynerContext : DbContext, IUnitOfWork
    {

        public const string DEFAULT_SCHEMA = "dbo";
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public DbSet<Domain.AggregatesModel.ProductAggregate.Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Domain.AggregatesModel.AttributeAggregate.Attribute> Attribute { get; set; }
        public DbSet<AttributeValue> AttributeValue { get; set; }
        public DbSet<Currency> Currency { get; set; }


        public BoynerContext() { }
        public BoynerContext(DbContextOptions<BoynerContext> options) : base(options) { }
        public BoynerContext(DbContextOptions<BoynerContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("BoynerContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();
            builder.UseSqlServer();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Database Default Values
            var turkishLiraCurrency = new Currency(Guid.NewGuid(), "Turkish Lira", "TL");
            modelBuilder.Entity<Currency>().HasData(turkishLiraCurrency);
            var amerikanDollarCurrency = new Currency(Guid.NewGuid(), "American Dollar", "USD");
            modelBuilder.Entity<Currency>().HasData(amerikanDollarCurrency);

            modelBuilder.Entity<ProductStatus>().HasData(
new ProductStatus(1, "Active"));
            modelBuilder.Entity<ProductStatus>().HasData(
new ProductStatus(2, "Passive"));

            modelBuilder.Entity<CategoryStatus>().HasData(
new ProductStatus(1, "Active"));
            modelBuilder.Entity<CategoryStatus>().HasData(
new ProductStatus(2, "Passive"));


            var colorAttributeId = Guid.NewGuid();
            modelBuilder.Entity<Domain.AggregatesModel.AttributeAggregate.Attribute>().HasData(new Domain.AggregatesModel.AttributeAggregate.Attribute(colorAttributeId, "Color"));
            var whiteAttributeValueId = Guid.NewGuid();
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(whiteAttributeValueId, "White", colorAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "Black", colorAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "Red", colorAttributeId));

            var brandAttributeId = Guid.NewGuid();
            modelBuilder.Entity<Domain.AggregatesModel.AttributeAggregate.Attribute>().HasData(new Domain.AggregatesModel.AttributeAggregate.Attribute(brandAttributeId, "Brand"));

            var nikeAttributeId = Guid.NewGuid();
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(nikeAttributeId, "Nike", brandAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "Tommy Hilfiger", brandAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "Sneckhers", brandAttributeId));

            var sizeAttributeId = Guid.NewGuid();
            modelBuilder.Entity<Domain.AggregatesModel.AttributeAggregate.Attribute>().HasData(new Domain.AggregatesModel.AttributeAggregate.Attribute(sizeAttributeId, "Size"));
            var xlAttributeId = Guid.NewGuid();
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "S", sizeAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "M", sizeAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "L", sizeAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(xlAttributeId, "XL", sizeAttributeId));
            modelBuilder.Entity<AttributeValue>().HasData(new AttributeValue(Guid.NewGuid(), "2XL", sizeAttributeId));

            var clothesCategoryId = Guid.NewGuid();
            modelBuilder.Entity<Category>().HasData(new Category(clothesCategoryId, "Clothes"));

            modelBuilder.Entity<CategoryAttribute>().HasData(
           new CategoryAttribute(Guid.NewGuid(), clothesCategoryId, colorAttributeId));
            modelBuilder.Entity<CategoryAttribute>().HasData(
           new CategoryAttribute(Guid.NewGuid(), clothesCategoryId, sizeAttributeId));

            Guid productId = Guid.NewGuid();
            modelBuilder.Entity<Domain.AggregatesModel.ProductAggregate.Product>().HasData(new Domain.AggregatesModel.ProductAggregate.Product(productId, "Tshirt", 150, clothesCategoryId, turkishLiraCurrency.Id));

            modelBuilder.Entity<ProductAttribute>().HasData(new ProductAttribute(Guid.NewGuid(), productId, whiteAttributeValueId));
            modelBuilder.Entity<ProductAttribute>().HasData(new ProductAttribute(Guid.NewGuid(), productId, nikeAttributeId));
            modelBuilder.Entity<ProductAttribute>().HasData(new ProductAttribute(Guid.NewGuid(), productId, xlAttributeId));
            #endregion

            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductStatusEntityConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryStatusEntityConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            int result = await base.SaveChangesAsync(cancellationToken);

            if (result > 0)
                await _mediator.DispatchDomainEventsAsync(this);

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            return true;
        }

        /// <summary>
        /// WARNING! 
        /// This method can save all changes without raising any events!
        /// Use it just in transaction scope or non-domain process!
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                int result = await SaveChangesAsync();

                transaction.Commit();

                if (result > 0)
                    await _mediator.DispatchDomainEventsAsync(this);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
