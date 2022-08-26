using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate.Specifications;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAttributeRepository _attributeRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category(Guid.NewGuid(), request.Name);
            var category = await _categoryRepository.AddAsync(newCategory);
            if (request.CategoryAttributeIdList.Any())
            {
                foreach (var categoryAttributeId in request.CategoryAttributeIdList)
                {
                    var attributeSpec = new AttributeSpecification(categoryAttributeId);
                    var attribute = await _attributeRepository.GetBySpecAsync(attributeSpec);
                    if (attribute == null)
                        throw new ApplicationException($"Category Attribute Error. Id: {categoryAttributeId}");

                        var newCategoryAttribute = new CategoryAttribute(category.Id, attribute.Id);
                        category.AddCategoryAttribute(newCategoryAttribute);
                }
            }

            using var transaction = await _categoryRepository.UnitOfWork.BeginTransactionAsync();
            await _categoryRepository.AddAsync(category, cancellationToken);
            await _categoryRepository.UnitOfWork.CommitTransactionAsync(transaction);

            return new CreateCategoryDto
            {
                Id = category.Id
            };
        }
    }
}
