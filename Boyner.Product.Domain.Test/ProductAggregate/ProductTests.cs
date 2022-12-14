using Boyner.Product.Domain.SharedKernel.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Boyner.Product.Domain.Test.ProductAggregate
{
    public class ProductTests
    {
        [Fact]
        public void name_should_not_be_null()
        {
            var category = SetTestCategory();
            var currency = SetTestCurrency();
            try
            {
                var product = new AggregatesModel.ProductAggregate.Product(Guid.NewGuid(), "", 10, category.Id, currency.Id);
            }
            catch (Exception ex)
            {
                Assert.Equal("The string cannot be empty.", ex.Message);
            }
        }

        [Fact]
        public void id_should_not_be_empty()
        {
            var category = SetTestCategory();
            var currency = SetTestCurrency();
            try
            {
                var product = new AggregatesModel.ProductAggregate.Product(Guid.Empty, "Test Name", 10, category.Id, currency.Id);
            }
            catch (Exception ex)
            {
                Assert.Equal("The argument must have value.", ex.Message);
            }
        }

        [Fact]
        public void price_should_not_be_negative_or_Zero()
        {
            var category = SetTestCategory();
            var currency = SetTestCurrency();
            try
            {
                var product = new AggregatesModel.ProductAggregate.Product(Guid.NewGuid(), "Test Name", 0, category.Id, currency.Id);
            }
            catch (Exception ex)
            {
                Assert.Equal("The number must be positive.", ex.Message);
            }
        }

        [Fact]
        public void product_create_should_be_success()
        {
            var category = SetTestCategory();
            var currency = SetTestCurrency();
            var productName = "Test Name";
            decimal price = 1;
            Guid productId = Guid.NewGuid();

            var product = new AggregatesModel.ProductAggregate.Product(productId, productName, price, category.Id, currency.Id);
            Assert.NotNull(product);
            Assert.Equal(price, product.Price);
            Assert.Equal(category, product.Category);
            Assert.Equal(productName, product.Name);
            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public void category_should_not_be_null()
        {
            var currency = SetTestCurrency();
            try
            {
                var product = new AggregatesModel.ProductAggregate.Product(Guid.NewGuid(), "Test Name", 1, Guid.Empty, currency.Id);
            }
            catch (Exception ex)
            {
                Assert.Equal("Value cannot be null. (Parameter 'category')", ex.InnerException?.Message);
            }
        }

        [Fact]
        public void currency_should_not_be_null()
        {
            var category = SetTestCategory();
            try
            {
                var product = new AggregatesModel.ProductAggregate.Product(Guid.NewGuid(), "Test Name", 1, category.Id, Guid.Empty);
            }
            catch (Exception ex)
            {
                Assert.Equal("Value cannot be null. (Parameter 'currency')", ex.InnerException?.Message);
            }
        }

        private AggregatesModel.CategoryAggregate.Category SetTestCategory()
        {
            return new AggregatesModel.CategoryAggregate.Category(Guid.NewGuid(), "Test Category");
        }

        private AggregatesModel.ProductAggregate.Currency SetTestCurrency()
        {
            return new AggregatesModel.ProductAggregate.Currency(Guid.NewGuid(), "Turkish Lira", "TL");
        }
    }
}
