using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IResponseWrapper<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResponseWrapper<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var spec = new CategorySpecification(request.Id);
            var category = await _categoryRepository.GetBySpecAsync(spec);
            if (category == null)
            {
                throw new ApplicationException("Category Not Found");
            }
            category.Delete();
            await _categoryRepository.UpdateAsync(category);
            var result = await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new ResponseWrapper<bool>(result);
        }
    }
}
