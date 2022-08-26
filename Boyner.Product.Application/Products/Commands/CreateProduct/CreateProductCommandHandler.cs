using Boyner.Product.Application.Products.Commands.CreateProduct;
using Boyner.Product.Application.SeedWork.Models;

using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResponseWrapper<Guid>>
    {
        private readonly ILogger<CreateProductCommand> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IReadRepository<Category> _categoryRepository;
        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IReadRepository<Category> categoryRepository,
            ILogger<CreateProductCommand> logger)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._logger = logger;
        }
        public async Task<IResponseWrapper<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Category category = await _categoryRepository.GetBySpecAsync(new CategorySpecification(request.CategoryId), cancellationToken);

            //if (category == null)
            //    throw new ApplicationException("Category not found. Product did not created");

            //Guid productId = Guid.NewGuid();

            //Product product = new Product(productId, request.Name, company, brand, category, request.SKU, request.StockQuantity, request.AvailableForQuote, request.Order, request.Price);

            //await _productRepository.AddAsync(product, cancellationToken);

            //var result = await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            //return new ResponseWrapper<Guid>(product.Id, isSuccess: result);
            return null;
        }
    }
}