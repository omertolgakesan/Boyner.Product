using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;

namespace Boyner.Product.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<IResponseWrapper<Guid>>
    {
        public string Name { get; init; }
        public Guid CategoryId { get; init; }
        public decimal Price { get; init; }
    }
}
