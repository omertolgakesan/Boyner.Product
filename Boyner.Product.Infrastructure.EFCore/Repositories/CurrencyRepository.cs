using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    public class CurrencyRepository : EFRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(BoynerContext context) : base(context)
        { }

        public async Task<int> TotalCountAsync() => await _context.Category.CountAsync(x => x.DeletedOn == null);
    }

}
