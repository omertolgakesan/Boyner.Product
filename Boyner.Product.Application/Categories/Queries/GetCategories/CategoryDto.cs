using AutoMapper;
using Boyner.Product.Application.SeedWork.Mappings;
using Boyner.Product.Domain.AggregatesModel.AttributeAggregate;
using Boyner.Product.Domain.AggregatesModel.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyner.Product.Application.Categories.Queries.GetCategories
{
    public partial class CategoryDto
    {
        public CategoryDto()
        {
            this.CategoryAttributes = new Dictionary<string, List<string>>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, List<string>> CategoryAttributes { get; set; }

    }
}
