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
        private List<ProductListItem> productList;

        
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

        // an element that goes into an order's list of products
        struct ProductListItem
        {
            Product product;
            public int quantity { get; set; }

            // product price * quantity, return total price of this item
            double getTotalPrice()
            {
                return 0;
            }
        }
    }
}
