using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    public class ProductRepository : EFRepository<Domain.AggregatesModel.ProductAggregate.Product>, IProductRepository
    {
        public ProductRepository(BoynerContext context) : base(context)
        { }

        public async Task<int> TotalCountAsync() => await _context.Product.CountAsync(x => x.DeletedOn == null);
    }

}
