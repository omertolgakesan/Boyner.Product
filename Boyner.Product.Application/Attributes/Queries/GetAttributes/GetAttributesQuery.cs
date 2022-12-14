using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Attributes.Queries.GetAttributes
{
    public class GetAttributesQuery : IRequest<IResponseWrapper<List<AttributeDto>>>
    {
    }
}
