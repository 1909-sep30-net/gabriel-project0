using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        // The customer that placed this order
        public Customer customer { get; set; }

        // Store location that the order is for
        public Location store { get; set; }

        
        /* product list
         * - every item in this list should have a quantity
         * - list of <product, quantity> (and price?)
         */
        private List<Tuple<Product,int>> productList;

        


    }
}
