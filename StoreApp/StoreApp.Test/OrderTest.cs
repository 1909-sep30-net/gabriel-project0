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
        Product product = new Product();

        [Fact]
        public void IsValidOrder_EmptyProductList_False()
        {
            validCustomer.Name = "Billy Bob";
            
            order.MyCustomer = validCustomer;
            order.MyLocation = validLocation;

            //Assert.False(order.IsValidOrder());
            Assert.False(OrderManager.IsValidOrder(order));
        }

        [Fact]
        public void IsValidOrder_QuantityZeroInList_False()
        {
            validCustomer.Name = "Billy Bob";
            order.MyCustomer = validCustomer;
            order.MyLocation = validLocation;
            order.ProductList.Add(new Tuple<Product, int>(product, 0));


            Assert.False(order.IsValidOrder());

        }

        [Fact]
        public void IsValidOrder_MissingCustomer_False()
        {
            order.MyLocation = new Location();
            order.ProductList.Add(new Tuple<Product, int>(product, 5));

            Assert.False(order.IsValidOrder());
        }

        [Fact]
        public void IsValidOrder_ValidOrder_True()
        {
            validCustomer.Name = "Billy Bob";
            order.MyCustomer = validCustomer;
            order.MyLocation = validLocation;
            order.ProductList.Add(new Tuple<Product, int>(product, 5));

            Assert.True(order.IsValidOrder());

        }
    }
}
