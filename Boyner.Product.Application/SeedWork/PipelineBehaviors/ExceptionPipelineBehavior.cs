using Boyner.Product.Application.SeedWork.Exceptions;
using Boyner.Product.Domain.SharedKernel.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boyner.Product.Application.SeedWork.PipelineBehaviors
{
    public class ExceptionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public ExceptionPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (FluentValidation.ValidationException)
            {
                throw;
            }
            catch (DomainException)
            {
                throw;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("----- An exception occured. Handling ExceptionPipelineBehavior. {RequestClassName}: {@Request}. {ResponseClassName}: {@Exception}", typeof(TRequest).Name, @request, typeof(TResponse).Name, ex);

                throw new UndefinedApplicationException(ex.Message, ex);
            }
        }
    }
}
