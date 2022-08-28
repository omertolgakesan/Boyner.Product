using Boyner.Product.Application.SeedWork.PipelineBehaviors;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using Boyner.Product.Infrastructure.EFCore.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMemoryCache();

            services.AddScoped(typeof(IReadRepository<>), typeof(CachedRepository<>));
            services.AddScoped(typeof(EFRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            return services;
        }
    }
}
