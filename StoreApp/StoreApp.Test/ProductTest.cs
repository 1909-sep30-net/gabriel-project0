using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class ProductTest
    {
        // Set up customer object for every test to use
        Product product = new Product();

        [Fact]
        public void Name_Null_ThrowsNullArgumentException()
        {
            string newName = null;

            Assert.Throws<ArgumentNullException>(() => product.Name = newName);
        }


        [Fact]
        public void Name_Empty_ThrowsArgumentException()
        {
            string newName = "";

            Assert.Throws<ArgumentException>(() => product.Name = newName);
        }

        [Theory]
        [InlineData("Happy Doap Soap")]
        [InlineData("Mad Green Supreme")]
        [InlineData("MetaMix")]
        public void Name_ValidString_StoresNameCorrectly(string newName)
        {
            product.Name = newName;

            Assert.Equal(newName, product.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Price_ZeroOrNegative_ThrowArgumentException(decimal price)
        {
            Assert.Throws<ArgumentException>(() => product.Price = price);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100.567)]
        public void Price_Positive_StoresPriceCorrectly(decimal price)
        {
            product.Price = price;
            Assert.Equal(price, product.Price);
        }
    }
}
