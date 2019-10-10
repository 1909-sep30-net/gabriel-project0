using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        /// <summary>
        /// The customer that placed this order
        /// </summary>
        public Customer MyCustomer { get; set; }

        /// <summary>
        /// Store location that the order is for
        /// </summary>
        public Location MyLocation { get; set; }

        /// <summary>
        /// List of products and their quantity to be ordered
        /// </summary>
        public List<Product> ProductList { get; set; } = new List<Product>();

        /// <summary>
        /// Time that the order was sent
        /// </summary>
        /// <return>Null if order has not been sent yet</return>
        public DateTime MyTime { get; set; }

    }
}
