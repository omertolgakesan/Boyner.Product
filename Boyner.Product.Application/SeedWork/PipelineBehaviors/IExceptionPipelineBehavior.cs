using MediatR;

namespace Boyner.Product.Application.SeedWork.PipelineBehaviors
{
    public interface IExceptionPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
    }
}