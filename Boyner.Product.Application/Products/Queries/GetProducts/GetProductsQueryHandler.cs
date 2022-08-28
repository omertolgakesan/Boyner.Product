using AutoMapper;
using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IResponseWrapper<IEnumerable<ProductDto>>>
    {
        private readonly IReadRepository<Domain.AggregatesModel.ProductAggregate.Product> _productRepository;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IReadRepository<Domain.AggregatesModel.ProductAggregate.Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
        }

        public async Task<IResponseWrapper<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductSpecification(request.Name, request.PrizeRangeModel?.MinimumPrice, request.PrizeRangeModel?.MaximumPrice, request.CategoryName);

            IEnumerable<Domain.AggregatesModel.ProductAggregate.Product> products = await _productRepository.ListAsync(spec, cancellationToken);

            var mappedProducts = _mapper.Map<IEnumerable<Domain.AggregatesModel.ProductAggregate.Product>, IEnumerable<ProductDto>>(products);
            foreach (var mappedProduct in mappedProducts)
            {
                var dictionary = new Dictionary<string, string>();
                foreach (var item in mappedProduct.ProductAttributeKey)
                {
                    dictionary.Add(item.Key, item.Value);
                }
                mappedProduct.ProductAttributess = dictionary;
            }

            return new ResponseWrapper<IEnumerable<ProductDto>>(mappedProducts);
        }
    }
}
