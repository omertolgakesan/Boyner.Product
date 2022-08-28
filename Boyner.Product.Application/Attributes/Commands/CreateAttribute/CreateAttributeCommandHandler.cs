using Boyner.Product.Application.SeedWork.Models;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using MediatR;

namespace Boyner.Product.Application.Attributes.Commands.CreateAttribute
{

    public class CreateAttributeCommandHandler : IRequestHandler<CreateAttributeCommand, IResponseWrapper<Guid>>
    {
        private readonly IAttributeRepository _attributeRepository;

        public CreateAttributeCommandHandler(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }
        public async Task<IResponseWrapper<Guid>> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {
            var attribute = new Domain.AggregatesModel.AttributeAggregate.Attribute(Guid.NewGuid(), request.Name);
            attribute.AddAttributeValues(request.AttributeValues);

            using var transaction = await _attributeRepository.UnitOfWork.BeginTransactionAsync();
            await _attributeRepository.AddAsync(attribute);
            await _attributeRepository.UnitOfWork.CommitTransactionAsync(transaction);

            return new ResponseWrapper<Guid>(attribute.Id);
        }
    }
}
