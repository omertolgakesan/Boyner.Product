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
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IResponseWrapper<List<CategoryDto>>>
    {
        private readonly IReadRepository<Category> _categoryRepository;

        public GetCategoriesQueryHandler(IReadRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResponseWrapper<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categorySpec = new CategorySpecification();
            var categories = await _categoryRepository.ListAsync(categorySpec, cancellationToken);

            if (categories == null)
            {
                throw new ApplicationException("Categories not found");
            }

            var retVal = new List<CategoryDto>();

            foreach (var category in categories)
            {
                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                };
                foreach (var categoryAttribute in category.CategoryAtrributes)
                {
                    categoryDto.CategoryAttributes.TryAdd(categoryAttribute.Attribute.Name, categoryAttribute.Attribute.AttributeValues.Select(y => y.Name).ToList());
                }
                retVal.Add(categoryDto);
            }
            return new ResponseWrapper<List<CategoryDto>>(retVal);
        }
    }
}
