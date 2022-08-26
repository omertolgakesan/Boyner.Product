using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CreateCategoryDto>
    {
        public string Name { get; set; }
        public List<Guid> CategoryAttributeIdList { get; set; }
    }
}
