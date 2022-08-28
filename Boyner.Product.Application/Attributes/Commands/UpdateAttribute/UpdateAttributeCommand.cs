using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Commands.UpdateAttribute
{
    public class UpdateAttributeCommand : IRequest<IResponseWrapper<bool>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
