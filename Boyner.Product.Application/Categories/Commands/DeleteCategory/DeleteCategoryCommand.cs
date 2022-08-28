using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<IResponseWrapper<bool>>
    {
        public Guid Id { get; set; }
    }
}
