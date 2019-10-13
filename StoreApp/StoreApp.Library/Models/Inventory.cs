using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Inventory
    {


        /// <summary>
        /// Invetories are initiazlied with an empty list
        /// List items comprise of a product and a corresponding quantity
        /// </summary>
        //private List<Item> MyList = new List<Item>();

        public List<Item> MyList { get; set; }

        /// <summary>
        /// Checks if given product list contains a product with given name
        /// </summary>
        /// <param name="name">Name of product to be searched for</param>
        /// <param name="pl">Product list to be searched</param>
        /// <returns>True if contains, False if not</returns>
        public bool Contains(Product product)
        {
            foreach (Item i in MyList)
            {
                if (i.product.ID == product.ID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a list item in the inventory matching with given product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Returns the list item containing the given product; Null if not found</returns>
        public Item GetItem(Product product)
        {
            foreach (Item i in MyList)
            {
                if (product.ID == i.product.ID)
                {
                    return i;
                }
            }

            // Returns null if product was not found in list
            return null;
        }

        /// <summary>
        /// Adds a product to the inventory; a new reference to the product is added if it didnt already
        /// exist, otherwise just increases a product's quantity
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void Add(Product product, int quantity)
        {
            // Validate quantity
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity added should be 1 or more.", nameof(quantity));
            }

            // Cannot add a null product
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null.", nameof(product));
            }

            // Fetch the item with specified  product from inventory's list
            Item listItem = GetItem(product);

            // If inventory does not have the item, add a new item with that product and quantity
            if (listItem == null)
            {
                listItem = new Item
                {
                    product = product,
                    quantity = quantity
                };
                MyList.Add(listItem);
            }
            // If inventory already has product, increase quantity of that product
            else
            {
                listItem.quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a set amount of product from the inventory
        /// </summary>
        /// <remarks>
        /// If the set amount will reduce the product quantity to 0, remove product from the inventory
        /// </remarks>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void Remove(Product product, int quantity)
        {
            // Validate quantity
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity added should be 1 or more.", nameof(quantity));
            }

            // Cannot add a null product
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot be null.", nameof(product));
            }

            // Fetch the item with specified  product from inventory's list
            Item listItem = GetItem(product);

            // If inventory does not have the item, add a new item with that product and quantity
            if (listItem == null)
            {
                Console.WriteLine("Item does not exist in inventory.");
                throw new ArgumentException("Item does not exist in inventory.",nameof(product));
            }

            // Amount of product left after decreasing
            int quantityLeft = listItem.quantity - quantity;

            // If leftover quantity would be less than 0, throw exception
            if (quantityLeft < 0)
            {
                throw new ArgumentException("Quantity being removed is more than in inventory.", nameof(quantity));
            }
            // If leftover quantity would be 0, simply remove product from list
            if (quantityLeft == 0)
            {
                MyList.Remove(listItem);
            }
            // If leftoever quantity is some positive number, proceed with decreasing quantity
            if (quantityLeft > 0 )
            {
                listItem.quantity -= quantity;
            }


        }

        /// <summary>
        /// Returns the number of items in this inventory
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return MyList.Count;
        }

        /// <summary>
        /// Checks if this inventory is valid (non empty)
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (Count() > 1)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public class Item
        {
            public Product product { get; set; }
            public int quantity { get; set; }

        }
    }
}
