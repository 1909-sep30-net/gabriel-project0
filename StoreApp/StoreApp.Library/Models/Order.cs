using StoreApp.Library.Models;
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
        public Inventory ProductList { get; set; } = new Inventory();

        /// <summary>
        /// Time that the order was sent
        /// </summary>
        /// <return>Null if order has not been sent yet</return>
        public DateTime MyTime { get; set; }

        /// <summary>
        /// Returns whether or not this order is valid and can be submitted
        /// </summary>
        /// 
        /// <remarks>
        /// Checks:
        ///     - if ProductList size is at least >= 1
        ///     - if every item in ProductList has a corresponding quantity greater than 0
        ///     - if MyCustomer is set
        ///     - if MyLocation is set
        /// </remarks>
        /// 
        /// <returns>
        /// Returns true if a valid order, false if not.
        /// </returns>
        public bool IsValidOrder()
        {

            // Not valid if Customer or Location have not been set to order yet
            if (MyCustomer == null || MyLocation == null)
            {
                return false;
            }

            if (MyLocation.IsValid())
            {
                return false;
            }


            if (ProductList.IsValid())
            {
                return false;
            }

            return true;
        }
    }
}
