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

        public Library.Order GetMostRecentOrder()
        {
            var lastOrder = dbcontext.Orders.OrderByDescending(o=>o.OrderId).First();
            return Mapper.MapOrder(lastOrder);
        }

        public IEnumerable<Library.Order> GetOrdersWithProductsByCustomerID(int customerId)
        {
            var orders = dbcontext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Location)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Color)
                 .Select(Mapper.MapOrder)
                 .ToList();

            return orders;
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
                    .ThenInclude(p=>p.Color)
                .Select(Mapper.MapOrderItem)
                .ToList();
            return orderItems;

            //return dbcontext.Orders
            //    .Where(o => o.LocationId == id)
            //    .Include(o => o.Customer)
            //        .ThenInclude(oi => oi.Orders)
            //    .Select(Mapper.MapOrder)
            //    .ToList();
        }

        /// <summary>
        /// Add order to db
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Library.Order order)
        {
            var newEntity = Mapper.MapOrder(order);
            newEntity.OrderId = 0; // So there's no primary key conflicts
            dbcontext.Orders.Add(newEntity);
        }

        /// <summary>
        /// Adds order items to the db
        /// </summary>
        /// <param name="orderItems">List of items in an order to be submitted to db</param>
        public void AddOrderItems(IEnumerable<Library.Item> orderItems, Library.Order order)
        {

            // populate list with mapped items
            foreach (Library.Item item in orderItems)
            {
                // find associated inventory item using item id
                var newEntity = Mapper.MapOrderItem(item, order);
                dbcontext.OrderItems.Add(newEntity);
            }
        }

        /// <summary>
        /// Save changes to the database!!!
        /// </summary>
        public void SaveChanges()
        {
            dbcontext.SaveChanges();
        }

    }
}
