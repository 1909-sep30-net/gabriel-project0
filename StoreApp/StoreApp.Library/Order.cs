using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    class Order
    {
        public Customer customer { get; set; }

        // Time that the order is placed
        public DateTime orderTime { get; set; }

        // Store location that the order is for
        public Location store { get; set; }

        /* product list
         * - every item in this list should have a quantity
         * - list of <product, quantity> (and price?)
         */
        private List<ProductListItem> productList;

        // Returns total price of the order
        public int ReturnTotalPrice() 
        {
            return 0;
        }

        // Add product to product list
        public void AddProduct(Product product, int quantity)
        {

        }

        // an element that goes into an order's list of products
        struct ProductListItem
        {
            Product product;
            int quantity;

            // product price * quantity, return total price of this item
            double getTotalPrice()
            {
                return 0;
            }
        }
    }
}
