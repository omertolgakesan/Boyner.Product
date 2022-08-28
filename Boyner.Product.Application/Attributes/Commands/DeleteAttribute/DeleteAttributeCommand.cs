using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Commands.DeleteAttribute
{
    public class DeleteAttributeCommand : IRequest<IResponseWrapper<bool>>
    {
        public Guid Id { get; set; }
    }
}
