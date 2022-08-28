using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Commands.CreateAttribute
{
    public class CreateAttributeCommand : IRequest<IResponseWrapper<Guid>>
    {
        public CreateAttributeCommand()
        {
            this.AttributeValues = new List<string>();
        }
        public string Name { get; set; }
        public List<string> AttributeValues { get; set; }
    }
}
