using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Repositories
{
    public class OrderLogRepository
    {
        /// <summary>
        /// Adds an order to the log and attaches a time stamp to the order
        /// </summary>
        /// <param name="order">Ahhh</param>
        public void Add(Order order)
        {
            DateTime time = DateTime.Now;
            var addToLog = new Tuple<Order, DateTime>(order, time);
            //orders.Add(addToLog);

        }

        /// <summary>
        /// Prints the contents of this log to console
        /// 
        /// Product and DateTime of each order
        /// </summary>
        public void print()
        {

        }

        public void SortByDate()
        {

        }

        public void SortByPrice()
        {

        }
    }
}
