using Boyner.Product.Application.SeedWork.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Commands.UpdateAttribute
{
    public class UpdateAttributeCommandValidation : AbstractValidator<UpdateAttributeCommand>
    {
        public UpdateAttributeCommandValidation()
        {
            RuleFor(x=>x.Id).SetValidator(new GuidValidator<UpdateAttributeCommand, Guid>()).WithMessage("Invalid Attribute Id");
        }
    }
}
