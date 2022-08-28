using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate.Specifications;
using MediatR;

namespace Boyner.Product.Application.Attributes.Commands.UpdateAttribute
{

    public class UpdateAttributeCommandHandler : IRequestHandler<UpdateAttributeCommand, IResponseWrapper<bool>>
    {
        private readonly IAttributeRepository _attributeRepository;

        public UpdateAttributeCommandHandler(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public async Task<IResponseWrapper<bool>> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var spec = new AttributeSpecification(request.Id);
            var attribute = await _attributeRepository.GetBySpecAsync(spec);
            if (attribute == null)
                throw new ApplicationException("Attribute not found");

            attribute.UpdateName(request.Name);
            await _attributeRepository.UpdateAsync(attribute);
            var result = await _attributeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new ResponseWrapper<bool>(result);
        }
    }
}
