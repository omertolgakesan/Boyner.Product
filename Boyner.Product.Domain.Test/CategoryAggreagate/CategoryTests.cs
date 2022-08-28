using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boyner.Product.Domain.Test.CategoryAggreagate
{
    public class CategoryTests
    {
        [Fact]
        public void name_should_not_be_null()
        {
            try
            {
                var product = new AggregatesModel.CategoryAggregate.Category(Guid.NewGuid(), string.Empty);
            }
            catch (Exception ex)
            {
                Assert.Equal("The string cannot be empty.", ex.Message);
            }
        }

        [Fact]
        public void id_should_not_be_empty()
        {
            try
            {
                var product = new AggregatesModel.CategoryAggregate.Category(Guid.Empty, "Test Name");
            }
            catch (Exception ex)
            {
                Assert.Equal("The argument must have value.", ex.Message);
            }
        }

        [Fact]
        public void category_create_should_be_success()
        {
            var categoryName = "Test Name";
            Guid categoryId = Guid.NewGuid();

            var category = new AggregatesModel.CategoryAggregate.Category(categoryId, categoryName);
            Assert.NotNull(category);
            Assert.Equal(categoryName, category.Name);
            Assert.Equal(categoryId, category.Id);
        }
    }
}
