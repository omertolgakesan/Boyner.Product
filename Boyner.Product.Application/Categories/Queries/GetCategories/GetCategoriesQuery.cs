
using Boyner.Product.Application.Categories.Queries.GetCategories;
using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<IResponseWrapper<List<CategoryDto>>>
    {
    }
}
