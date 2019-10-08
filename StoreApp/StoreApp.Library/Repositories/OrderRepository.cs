using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Repositories
{
    public class OrderRepository
    {
        /*
 * Returns total price of the order
 * - iterates through productList, adds up each item's price
 *   and returns the sum
 *   
 * Edge Cases:
 *  - if list is empty
 */
        public int ReturnTotalPrice()
        {
            return 0;
        }

        // Add product to product list
        /*
         * Add product to product list
         * 
         * Edge Cases:
         *  - product is already in current order
         *  - quantity is too high or <= 0
         * 
         */
        public void AddProduct(Product product, int quantity)
        {

        }

        /*
         * Remove particular product from list
         * 
         * Edge Cases:
         *  - product is in the current order
         *  - the specified quantity is available to remove
         * 
         */
        public void removeProduct(Product product, int quantity)
        {

        }

        /*
         * Clear current order; resets the list
         */
        public void clearOrder()
        {

        }

        /*
         * Checks if order is valid
         */
        public bool Validate()
        {

            return false;
        }
    }
}
