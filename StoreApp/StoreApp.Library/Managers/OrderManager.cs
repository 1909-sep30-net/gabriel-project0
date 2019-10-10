using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Managers
{
    public static class OrderManager
    {
        /// <summary>
        /// Add product to product list
        /// </summary>
        /// <param name="order"></param>
        /// <param name="quantity"></param>
        public static void AddProduct(Order order, Product product, int quantity)
        {           
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity added should be 1 or more.", nameof(quantity));
            }
            if (order == null)
            {
                throw new ArgumentNullException("Order cannot be null.", nameof(order));
            }
            
            // If product is already in order's list, just increment that particular product's quantity
            if (ProductListContains(product.Name,order.ProductList))
            {
                GetProduct(product.Name, order.ProductList).Quantity += quantity;
            }
            // If product isn't already in order's list, add new product item to that order's list
            else
            {
                product.Quantity = quantity;
                order.ProductList.Add(product);
            }
        }

        public static void RemoveProduct(Order order, Product product, int quantity)
        {
            // Check for valid quantity and order
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity removed should be 1 or more.", nameof(quantity));
            }
            if (order == null)
            {
                throw new ArgumentNullException("Order cannot be null.", nameof(order));
            }

            // If product is not already in order's list, don't do anything (but also display some sort of message?)
            if (!ProductListContains(product.Name, order.ProductList))
            {
                Console.WriteLine("Nothing removed, product does not exist in this order.");
            }
            else
            {
                // Grab the product from the order productlist
                Product productInOrder = GetProduct(product.Name, order.ProductList);

                // If the quantity being removed completely depletes product's quantity, just remove it from list
                if (productInOrder.Quantity - quantity <= 0)
                {
                    order.ProductList.Remove(productInOrder);
                }

                // Otherwise, just subtract form product's quantity
                else
                {
                    productInOrder.Quantity -= quantity;
                }
                
            }
        }

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
        public static bool IsValidOrder(Order order)
        {

            // Not valid if Customer or Location have not been set to order yet
            if (order.MyCustomer == null || order.MyLocation == null)
            {
                return false;
            }

            if (!LocationManager.IsValidLocation(order.MyLocation))
            {
                return false;
            }


            //// TODO:
            //// Order is invalid if MyCustomer or MyLocation is invalid
            //if (!MyCustomer.IsValid() || !MyLocation.IsValid())
            //{
            //    return false;
            //}

            // If product list isnt valid, order isnt valid
            if (!IsValidProductList(order.ProductList))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if product list is considered valid
        /// </summary>
        /// <param name="pl">Product list to be tested</param>
        /// <returns>True if product list is valid, false if not</returns>
        public static bool IsValidProductList(List<Product> pl)
        {
            // List is valid if size larger than 0
            if (pl.Count < 1)
            {
                return false;
            }

            // If any item in ProductList has a quantity <= 0, order is invalid
            foreach (Product item in pl)
            {
                if (item.Quantity < 1)
                {
                    return false;
                }
            }
            
            return true;

        }

        /// <summary>
        /// Checks if given product list contains a product with given name
        /// </summary>
        /// <param name="name">Name of product to be searched for</param>
        /// <param name="pl">Product list to be searched</param>
        /// <returns>True if contains, False if not</returns>
        public static bool ProductListContains(string name, List<Product> pl)
        {
            foreach(Product product in pl)
            {
                if (product.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Obtain product of given name from given product list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pl"></param>
        /// <returns>Product in product list, null if not found</returns>
        public static Product GetProduct(string name, List<Product> pl)
        {

            if ( ProductListContains(name, pl) )
            {
                // A little bit redundant, but search for particular product again
                //  after confirming that list contains product
                foreach (Product product in pl)
                {
                    if (product.Name == name)
                    {
                        return product;
                    }
                }


            }
            return null;
        }

    }
}
