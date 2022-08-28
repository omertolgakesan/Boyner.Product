using Boyner.Product.Application.Products.Commands.CreateProduct;
using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate.Specifications;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate.Specifications;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
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
        private readonly IProductRepository _productRepository;
        private readonly IReadRepository<Category> _categoryRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IAttributeValueRepository _attributeValueRepository;
        private readonly ICurrencyRepository _currencyRepository;
        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IReadRepository<Category> categoryRepository,
            IAttributeRepository attributeRepository,
            IAttributeValueRepository attributeValueRepository,
            ICurrencyRepository currencyRepository)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._attributeRepository = attributeRepository;
            this._attributeValueRepository = attributeValueRepository;
            this._currencyRepository = currencyRepository;
        }
        public async Task<IResponseWrapper<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetBySpecAsync(new CategorySpecification(request.CategoryId, true), cancellationToken);

            if (category == null)
                throw new ApplicationException("Category not found. Product can't created");

            var currencySpec = new CurrencySpecification(request.CurrencyCode);
            var currency = await _currencyRepository.GetBySpecAsync(currencySpec, cancellationToken);
            if (currency == null)
            {
                throw new ApplicationException($"{request.CurrencyCode} not found.");
            }

            Domain.AggregatesModel.ProductAggregate.Product product = new Domain.AggregatesModel.ProductAggregate.Product(Guid.NewGuid(), request.Name, request.Price, category.Id, currency.Id);

            await _productRepository.AddAsync(product, cancellationToken);

            if (request.ProductAttributeValues.Any())
            {
                var attributeValueList = await _attributeValueRepository.ListAsync(new AttributeValueSpecification(request.ProductAttributeValues));
                if (attributeValueList == null)
                {
                    throw new ApplicationException("Attributes not found. Product can't created");
                }

                if (attributeValueList.Count != request.ProductAttributeValues.Count)
                {
                    var diffirenceAttributeIds = request.ProductAttributeValues.Except(attributeValueList.Select(x => x.Id).ToList());
                    string message = string.Join(",", diffirenceAttributeIds);
                    throw new ApplicationException($"Attribute(s) not found :{message}");
                }

                if (category.CategoryAtrributes.Count != attributeValueList.GroupBy(x => x.AttributeId).Count())
                {
                    throw new ApplicationException("Attributes not found. Product can't created");
                }

                foreach (var attributeValue in attributeValueList)
                {
                    product.AddAttributeValue(attributeValue);
                }
            }

            var result = await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new ResponseWrapper<Guid>(product.Id, isSuccess: result);
        }
    }
}