using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, IResponseWrapper<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResponseWrapper<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var spec = new CategorySpecification(request.Id);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            if (category == null)
            {
                throw new ApplicationException("Category Not Found");
            }

            category.UpdateName(request.Name);
            await _categoryRepository.UpdateAsync(category);
            var result = await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new ResponseWrapper<bool>(result);
        }
    }
}
