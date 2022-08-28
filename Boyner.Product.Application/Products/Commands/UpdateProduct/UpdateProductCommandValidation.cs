using Boyner.Product.Application.SeedWork.Validations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(command => command.ProductId).SetValidator(new GuidValidator<UpdateProductCommand, Guid>()).WithMessage("Invalid product id");
        }
    }
}
