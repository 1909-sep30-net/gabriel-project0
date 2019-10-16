using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.DataAccess.Repositories
{
    public class OrderRepository
    {
        private DoapSoapContext dbcontext;

        //CRUD
        public OrderRepository(DoapSoapContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context cant be null.", nameof(context));
            }
            dbcontext = context;
        }

        public IEnumerable<Library.Order> GetOrdersWithProductsByCustomerID(int id)
        {
            var orders = dbcontext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItem)
                .Where(o => o.CustomerId == id);

            return orders.Select(Mapper.MapOrder).ToList();
        }

        /// <summary>
        /// Gets all order items, including the product with them
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public IEnumerable<Library.Item> GetOrderItemsByOrderID(int orderid)
        {
            var orderItems = dbcontext.OrderItems
                .Where(oi => oi.OrderId == orderid)
                .Include(oi => oi.Product)
                .Select(Mapper.MapOrderItem)
                .ToList();
            return orderItems;
        }

    }
}
