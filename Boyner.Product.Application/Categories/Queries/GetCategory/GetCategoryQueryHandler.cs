using AutoMapper;
using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Queries.GetCategories
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IResponseWrapper<CategoryDto>>
    {
        private readonly IReadRepository<Category> _categoryRepository;

        public GetCategoryQueryHandler(IReadRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IResponseWrapper<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categorySpec = new CategorySpecification(request.CategoryId,true);
            var category = await _categoryRepository.GetBySpecAsync(categorySpec, cancellationToken);

            if (category == null)
            {
                throw new ApplicationException("Categories not Found");
            }

            var retVal = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
            foreach (var categoryAttribute in category.CategoryAtrributes)
            {
                retVal.CategoryAttributes.TryAdd(categoryAttribute.Attribute.Name, categoryAttribute.Attribute.AttributeValues.Select(x => x.Name).ToList());
            }

            return new ResponseWrapper<CategoryDto>(retVal);
        }
    }
}
