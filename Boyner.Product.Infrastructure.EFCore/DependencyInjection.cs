using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boyner.Product.Infrastructure.EFCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BoynerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BoynerDatabase")));

            return services;
        }
    }
}
