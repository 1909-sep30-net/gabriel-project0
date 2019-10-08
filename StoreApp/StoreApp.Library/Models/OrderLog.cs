using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class OrderLog
    {
        private ICollection<Tuple<Order, DateTime>> orders;

        // Time that the order is placed
        public DateTime orderTime { get; set; }


        /// <summary>
        /// Adds an order to the log
        /// </summary>
        /// <param name="order">Ahhh</param>
        public void Add(Order order)
        {
            DateTime time = DateTime.Now;
            var addToLog = new Tuple<Order, DateTime>(order, time);
            orders.Add(addToLog);

        }

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
