using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        /// <summary>
        /// Uniquely identifies which order this be
        /// </summary>
        public int OrderID { get; set; }

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

        public List<Item> ProductList { get; set; } = new List<Item>();

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
        ///     - if MyCustomer is set and valid
        ///     - if MyLocation is set and valid
        /// </remarks>
        /// 
        /// <returns>
        /// Returns true if a valid order, false if not.
        /// </returns>
        public bool IsValid()
        {
            
            // Not valid if Customer or Location have not been set to order yet
            if ( MyCustomer == null || MyLocation == null )
            {
                return false;
            }

            // Not valid if Location or Customer isn't valid
            if ( !MyLocation.IsValid() || !MyCustomer.IsValid() )
            {
                return false;
            }
            

            // If order's list doesn't have any items in it, order is invalid
            if (ProductList.Count <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prints the total amount of money this order is worth
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal()
        {
            decimal sum = 0;
            foreach (Item item in ProductList)
            {
                sum += item.Product.Price;
            }
            return sum;
        }

        /// <summary>
        /// Returns a list item in the order inventory matching with given product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Returns the list item containing the given product; Null if not found</returns>
        public Library.Item GetItem(Product product)
        {
            foreach (Library.Item i in ProductList)
            {
                if (product.ID == i.Product.ID)
                {
                    return i;
                }
            }

            // Returns null if product was not found in list
            return null;
        }

        /// <summary>
        /// Adds an item to order's product list, increasing quantity if it already exists
        /// </summary>
        /// <param name="item"></param>
        public void Add(Item item)
        {
            // Validate quantity
            if (item.Quantity < 1)
            {
                throw new ArgumentException("Quantity added should be 1 or more.", nameof(item.Quantity));
            }

            // Cannot add a null product
            if (item.Product == null)
            {
                throw new ArgumentNullException("Product cannot be null.", nameof(item.Product));
            }

            // Fetch the item with specified  product from inventory's list
            Library.Item listItem = GetItem(item.Product);

            // If inventory does not have the item, add a new item with that product and quantity
            if (listItem == null)
            {
                listItem = new Library.Item
                {
                    Product = item.Product,
                    Quantity = item.Quantity
                };
                ProductList.Add(listItem);
            }
            // If inventory already has product, increase quantity of that product
            else
            {
                listItem.Quantity += item.Quantity;
            }
        }
    }
}
