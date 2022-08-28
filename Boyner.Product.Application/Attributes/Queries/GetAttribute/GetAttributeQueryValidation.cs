using Boyner.Product.Application.Attributes.Queries.GetAttributes;
using Boyner.Product.Application.SeedWork.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Queries.GetAttribute
{
    public class GetAttributeQueryValidation : AbstractValidator<GetAttributeQuery>
    {
        public GetAttributeQueryValidation()
        {
            RuleFor(x=>x.Id).SetValidator(new GuidValidator<GetAttributeQuery, Guid>()).WithMessage("Invalid AttributeId");
        }
    }
}
