using AutoMapper;
using Boyner.Product.Application.SeedWork.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Queries.GetAttributes
{
    public partial class AttributeDto
    {
        public AttributeDto()
        {
            this.AttributeValues = new List<AttributeValueDto>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<AttributeValueDto> AttributeValues { get; set; }
    }

    public partial class AttributeDto : IMapFrom<Domain.AggregatesModel.AttributeAggregate.Attribute>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.AggregatesModel.AttributeAggregate.Attribute, AttributeDto>()
                .ForMember(x => x.AttributeValues, opt => opt.MapFrom(y => y.AttributeValues.Select(z => new AttributeValueDto { Id = z.Id, Name = z.Name })));
        }
    }

}
