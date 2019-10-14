using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Repositories
{
    public class OrderRepository
    {
        // Add product to product list
        /*
         * Add product to product list
         * 
         * Edge Cases:
         *  - product is already in current order
         *  - quantity is too high or <= 0
         * 
         */
        public void addProduct(Product product, int quantity)
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
         * Checks if order is valid
         */
        public bool Validate()
        {

            return false;
        }
    }
}
