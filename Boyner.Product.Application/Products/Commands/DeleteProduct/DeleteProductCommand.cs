using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<IResponseWrapper<bool>>
    {
        public Guid ProductId { get; set; }
    }
}
