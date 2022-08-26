using AutoMapper;
using Boyner.Product.Application.SeedWork.Mappings;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Queries.GetProducts
{
    public partial class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public List<KeyValuePair<string, string>> ProductAttributeKey { get; set; }
        public Dictionary<string, string> ProductAttributess { get; set; }
        public string Price { get; set; }
    }

    public partial class ProductDto : IMapFrom<Domain.AggregatesModel.ProductAggregate.Product>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.AggregatesModel.ProductAggregate.Product, ProductDto>()
                .ForMember(x => x.Price, opt => opt.MapFrom(s => $"{ s.Price } { s.Currency.CurrencyCode }"))
                 .ForMember(d => d.ProductAttributeKey, opt => opt.MapFrom(s => s.ProductAttributes.Select(x => new KeyValuePair<string, string>(x.Attribute.Name, x.AttributeValue.Name)).ToList()));
        }
    }
}
