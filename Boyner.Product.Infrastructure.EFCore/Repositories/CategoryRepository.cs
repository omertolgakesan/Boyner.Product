using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boyner.Product.Infrastructure.EFCore.Repositories
{
    public class CategoryRepository : EFRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BoynerContext context) : base(context)
        { }

        public async Task<int> TotalCountAsync() => await _context.Category.CountAsync(x => x.DeletedOn == null);
    }

}
