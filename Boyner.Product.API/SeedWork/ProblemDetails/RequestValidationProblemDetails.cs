using FluentValidation.Results;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Boyner.Product.API.SeedWork.ProblemDetails
{
    public class RequestValidationProblemDetails : ValidationProblemDetails
    {
        public RequestValidationProblemDetails(FluentValidation.ValidationException ex, HttpContext ctx) : base(GetErrors(ex.Errors))
        {
            Title = ex.Message;
            Detail = ex.InnerException?.Message; //TODO: It can be return by detail explanation. Exception needs to be contain Title & Detail message. Now we have just Title(Mesage)
            Type = UriHelper.GetDisplayUrl(ctx.Request);
            Instance = ctx.Request.Path;
            Status = StatusCodes.Status400BadRequest;
            //GetErrors(ex.Errors).ToList().ForEach(i => this.Errors.Add(i.Key, i.Value.ToArray()));
        }

        private static IDictionary<string, string[]> GetErrors(IEnumerable<ValidationFailure> failures)
        {
            var Failures = new Dictionary<string, string[]>();

            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Failures.Add(propertyName, propertyFailures);
            }

            return Failures;
        }
    }
}
