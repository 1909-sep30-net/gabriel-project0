using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class OrderTest
    {
        Order order = new Order();

        Customer customer = new Customer();
        Location location = new Location();
        Product product = new Product();
        Item item = new Item();

        /*
        [Theory]
        [InlineData("George Groose","Hope")]
        [InlineData("Mestrul Maggis","Another Place")]
        [InlineData("Regina George", "Underground")]

        public void IsValidOrder_ValidCustomerLocation_ReturnsTrue(string cusName, string locName)
        {

            item.Product = product;
            item.Quantity = 1;
            customer.Name = cusName;
            location.Name = locName;
            location.Inventory.Add(item);
            order.ProductList.Add(item);
            order.MyCustomer = customer;
            order.MyLocation = location;

            Assert.True(order.IsValid());
        }
        */
        [Fact]
        public void IsValidOrder_InvalidCustomer_ReturnsFalse()
        {
            location.Name = "Moe Town";
            order.MyCustomer = customer;
            order.MyLocation = location;

            Assert.False(order.IsValid());
        }

        [Fact]
        public void IsValidOrder_InvalidLocation_ReturnsFalse()
        {
            customer.Name = "Georgiana Crown";
            order.MyCustomer = customer;
            order.MyLocation = location;

            Assert.False(order.IsValid());
        }

        [Fact]
        public void IsValidOrder_NullLocation_ReturnsFalse()
        {

            customer.Name = "Two Pocks";
            order.MyCustomer = customer;

            Assert.False(order.IsValid());
        }

        [Fact]
        public void IsValidOrder_NullCustomer_ReturnsFalse()
        {

            location.Name = "Joe Town";
            order.MyLocation = location;

            Assert.False(order.IsValid());
        }

        /*
        [Fact]
        public void AddProduct_PositiveQuantity_StoreProductCorrectly()
        {
            OrderManager.AddProduct(order, validProduct, 10);

            Product productInOrder = OrderManager.GetProduct(validProduct.Name, order.ProductList);

            Assert.Equal(validProduct, productInOrder);
        }

        [Fact]
        public void IsValidOrder_EmptyProductList_False()
        {
            validCustomer.Name = "Billy Bob";

            order.MyCustomer = validCustomer;
            order.MyLocation = validLocation;

            Assert.False(OrderManager.IsValidOrder(order));
        }

        [Fact]
        public void IsValidOrder_MissingCustomer_False()
        {
            order.MyLocation = validLocation;
            OrderManager.AddProduct(order, validProduct, 5);

            Assert.False(OrderManager.IsValidOrder(order));
        }

        [Fact]
        public void IsValidProductList_EmptyList_False()
        {
            Assert.False(OrderManager.IsValidProductList(order.ProductList));
        }

        [Fact]
        public void IsValidProductList_PopulatedList_True()
        {
            OrderManager.AddProduct(order,validProduct,5);

            Assert.True(OrderManager.IsValidProductList(order.ProductList));
        }

        [Fact]
        public void IsValidOrder_ValidCustomerLocationProduct_True()
        {
            validCustomer.Name = "Billy Bob";
            order.MyCustomer = validCustomer;
            order.MyLocation = validLocation;
            OrderManager.AddProduct(order, validProduct, 5);


            Assert.True(OrderManager.IsValidOrder(order));

        }
        */
    }
}
