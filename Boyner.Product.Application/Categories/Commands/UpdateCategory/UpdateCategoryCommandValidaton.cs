using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands
{
    public class UpdateCategoryCommandValidaton : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidaton()
        {

        }
    }
}
