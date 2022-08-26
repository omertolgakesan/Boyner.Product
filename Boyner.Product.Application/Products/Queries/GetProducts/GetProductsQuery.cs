using Boyner.Product.Application.Products.Queries.GetProducts;
using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IResponseWrapper<IEnumerable<ProductDto>>>
    {
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public PrizeRangeModel? PrizeRangeModel { get; set; }

    }
}
