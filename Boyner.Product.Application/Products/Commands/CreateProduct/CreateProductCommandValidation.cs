using Boyner.Product.Application.SeedWork.Validations;
using Boyner.Product.Domain.AggregatesModel.ProductAggregate;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;

namespace Boyner.Product.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation(ILogger<CreateProductCommandValidation> logger)
        {           
            RuleFor(command => command.Name).NotEmpty().WithMessage("Product name required").MaximumLength(250).WithMessage("Product name too long");       
            RuleFor(command => command.CategoryId).SetValidator(new GuidValidator<CreateProductCommand, Guid>()).WithMessage("Invalid category code");

            logger.LogInformation("----- {ClassName} validation rules assigned", GetType().Name);
        }

        private bool ProductStatusInRange(int id)
        {
            try
            {
                ProductStatus status = ProductStatus.FromValue<ProductStatus>(id);

                if (status == null)
                    return false;

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
