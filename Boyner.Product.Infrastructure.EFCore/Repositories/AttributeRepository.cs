using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    public class AttributeRepository : EFRepository<Domain.AggregatesModel.AttributeAggregate.Attribute>, IAttributeRepository
    {
        public AttributeRepository(BoynerContext context) : base(context)
        { }

        public async Task<int> TotalCountAsync() => await _context.Attribute.CountAsync(x => x.DeletedOn == null);
    }

}
