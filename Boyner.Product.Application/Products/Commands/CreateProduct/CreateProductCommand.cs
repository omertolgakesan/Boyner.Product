using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boyner.Product.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<IResponseWrapper<Guid>>
    {
        public CreateProductCommand()
        {
            this.ProductAttributeValues = new List<Guid>();
        }
        public string Name { get; init; }
        public Guid CategoryId { get; init; }
        public decimal Price { get; init; }
        [JsonIgnore]
        public string CurrencyCode => "TL";
        public List<Guid> ProductAttributeValues { get; set; }
    }
}
