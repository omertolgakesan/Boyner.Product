using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IResponseWrapper<bool>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommand> _logger;
        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            ILogger<DeleteProductCommand> logger)
        {
            this._productRepository = productRepository;
            this._logger = logger;
        }
        public async Task<IResponseWrapper<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var spec = new ProductSpecification(request.ProductId);

            Domain.AggregatesModel.ProductAggregate.Product product = await _productRepository.GetBySpecAsync(spec, cancellationToken);
            if (product == null)
                throw new ApplicationException("Product not found. Product did not deleted");

            product.Delete();

            var result = await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new ResponseWrapper<bool>(result);
        }
    }
}
