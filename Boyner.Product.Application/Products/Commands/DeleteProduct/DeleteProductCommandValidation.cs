using Boyner.Product.Application.SeedWork.Validations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommandValidation : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidation(ILogger<DeleteProductCommandValidation> logger)
        {
            RuleFor(command => command.ProductId).SetValidator(new GuidValidator<DeleteProductCommand, Guid>()).WithMessage("Invalid product id");

            logger.LogInformation("----- {ClassName} validation rules assigned", GetType().Name);
        }
    }
}
