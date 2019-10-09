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
        public List<Tuple<Product, int>> ProductList { get; set; } = new List<Tuple<Product, int>>();

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
            // Order is invalid if productlist is empty, or there is no set customer, location,
            if (ProductList.Count < 1 || MyCustomer == null || MyLocation == null)
            {
                return false;
            }

            // If any item in ProductList has a quantity <= 0, order is invalid
            foreach (Tuple<Product,int> item in ProductList)
            {
                if (item.Item2 < 1)
                {
                    return false;
                }
            }

            return true;

        }


    }
}
