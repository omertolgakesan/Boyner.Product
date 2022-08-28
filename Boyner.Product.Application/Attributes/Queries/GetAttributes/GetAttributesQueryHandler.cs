using AutoMapper;
using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Queries.GetAttributes
{
    public class GetAttributesQueryHandler : IRequestHandler<GetAttributesQuery, IResponseWrapper<List<AttributeDto>>>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        public GetAttributesQueryHandler(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }
        public async Task<IResponseWrapper<List<AttributeDto>>> Handle(GetAttributesQuery request, CancellationToken cancellationToken)
        {
            var spec = new AttributeSpecification();
            var attributes = await _attributeRepository.ListAsync(spec, cancellationToken);
            var mappedattributes = _mapper.Map<List<Domain.AggregatesModel.AttributeAggregate.Attribute>, List<AttributeDto>>(attributes);

            return new ResponseWrapper<List<AttributeDto>>(mappedattributes);
        }
    }
}
