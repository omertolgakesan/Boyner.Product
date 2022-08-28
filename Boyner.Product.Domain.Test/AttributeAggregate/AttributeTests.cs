using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boyner.Product.Domain.Test.AttributeAggregate
{
    public class AttributeTests
    {
        [Fact]
        public void name_should_not_be_null()
        {
            try
            {
                var product = new AggregatesModel.AttributeAggregate.Attribute(Guid.NewGuid(), string.Empty);
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
                var product = new AggregatesModel.AttributeAggregate.Attribute(Guid.Empty, "Test Name");
            }
            catch (Exception ex)
            {
                Assert.Equal("The argument must have value.", ex.Message);
            }
        }

        [Fact]
        public void attribute_create_should_be_success()
        {
            var attributeName = "Test Name";
            Guid attributeId = Guid.NewGuid();

            var attribute = new AggregatesModel.AttributeAggregate.Attribute(attributeId, attributeName);
            Assert.NotNull(attribute);
            Assert.Equal(attributeName, attribute.Name);
            Assert.Equal(attributeId, attribute.Id);
        }
    }
}
