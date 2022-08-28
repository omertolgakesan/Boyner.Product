using Boyner.Product.Application.SeedWork.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidaton : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidaton()
        {
            RuleFor(x=>x.Id).SetValidator(new GuidValidator<DeleteCategoryCommand, Guid>()).WithMessage("Invalid Category Id");
        }
    }
}
