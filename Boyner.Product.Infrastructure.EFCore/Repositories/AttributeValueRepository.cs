using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    public class AttributeValueRepository : EFRepository<AttributeValue>, IAttributeValueRepository
    {
        public AttributeValueRepository(BoynerContext context) : base(context)
        { }

        public async Task<int> TotalCountAsync() => await _context.Category.CountAsync(x => x.DeletedOn == null);
    }

}
