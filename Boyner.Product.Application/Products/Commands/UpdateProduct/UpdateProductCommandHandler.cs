using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResponseWrapper<bool>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
        }
        public async Task<IResponseWrapper<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var spec = new ProductSpecification(request.ProductId);

            var product = await _productRepository.GetBySpecAsync(spec, cancellationToken);
            if (product == null)
                throw new ApplicationException("Product not found. Product did not deleted");

            var categorySpec = new CategorySpecification(request.CategoryId);
            var category = await _categoryRepository.GetBySpecAsync(categorySpec, cancellationToken);
            if (category == null)
            {
                throw new ApplicationException("Category not found.Product did not updated");
            }

            product.Update(request.Name, category, request.Price);
            await _productRepository.UpdateAsync(product, cancellationToken);
            var result = await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new ResponseWrapper<bool>(result);
        }
    }
}
