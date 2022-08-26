using Boyner.Product.Domain.SharedKernel.SeedWork;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Boyner.Product.API.SeedWork.ProblemDetails
{
    public class ProblemDetailsOptionsConfiguration : IConfigureOptions<ProblemDetailsOptions>
    {
        private IWebHostEnvironment Environment { get; }
        private IHttpContextAccessor HttpContextAccessor { get; }

        public ProblemDetailsOptionsConfiguration(IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            Environment = environment;
            HttpContextAccessor = httpContextAccessor;
        }
        public void Configure(ProblemDetailsOptions options)
        {
            // This is the default behavior; only include exception details in a development & stage environment.
            options.IncludeExceptionDetails = (ctx, details) => Environment.IsDevelopment() || Environment.IsStaging(); //using Microsoft.Extensions.Hosting;
            options.IncludeExceptionDetails = (ctx, details) => false;
            options.OnBeforeWriteDetails = (ctx, details) =>
            {
                // keep consistent with asp.net core conventions that adds a tracing value
                ProblemDetailsHelper.SetTraceId(details, HttpContextAccessor.HttpContext);
            };

            // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
            // This is useful if you have upstream middleware that needs to do additional handling of exceptions.
            options.Rethrow<NotSupportedException>();

            /* 
            Domain Exception
            */
            options.Map<DomainException>((ctx, ex) => new Microsoft.AspNetCore.Mvc.ProblemDetails()
            {
                Type = UriHelper.GetDisplayUrl(ctx.Request),
                Title = ex.Message,
                Detail = $"Domain Exception: {ex.InnerException?.Message }",
                Instance = ctx.Request.Path,
                Status = StatusCodes.Status400BadRequest
            });

            /* 
            Application Exception
            */
            options.Map<ApplicationException>((ctx, ex) => new Microsoft.AspNetCore.Mvc.ProblemDetails()
            {
                Type = UriHelper.GetDisplayUrl(ctx.Request),
                Title = ex.Message,
                Detail = $"Application Exception: { ex.InnerException?.Message }",
                Instance = ctx.Request.Path,
                Status = StatusCodes.Status400BadRequest
            });


            /* 
            FluentValidation
            */
            options.Map<FluentValidation.ValidationException>((ctx, ex) => new RequestValidationProblemDetails(ex, ctx));

            // This will map NotImplementedException to the 501 Not Implemented status code.
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

            // This will map HttpRequestException to the 503 Service Unavailable status code.
            options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

            // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
            // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        }
    }

    public static class ProblemDetailsHelper
    {
        public static void SetTraceId(Microsoft.AspNetCore.Mvc.ProblemDetails details, HttpContext httpContext)
        {
            // this is the same behaviour that Asp.Net core uses
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            details.Extensions["traceId"] = traceId;
        }
    }
}
