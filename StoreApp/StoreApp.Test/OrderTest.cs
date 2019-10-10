using StoreApp.Library;
using StoreApp.Library.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class OrderTest
    {
        Order order = new Order();
        Customer validCustomer = new Customer();
        Location validLocation = new Location();
        Product validProduct = new Product();



        [Fact]
        public void AddProduct_NonpositiveQuantity_ArgumentException()
        {
            validCustomer.Name = "Billy Bob";
            order.MyCustomer = validCustomer;
            order.MyLocation = validLocation;

            Assert.Throws<ArgumentException>(() => OrderManager.AddProduct(order, validProduct, 0));

        }

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
            order.MyLocation = new Location();
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
    }
}
