using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class OrderLog
    {
        private List<Order> orders;


        /// <summary>
        /// Adds an order to the log
        /// </summary>
        /// <param name="order">Ahhh</param>
        public void Add(Order order)
        {
            orders.Add(order);

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
