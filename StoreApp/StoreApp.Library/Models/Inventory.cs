using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Models
{
    public class Inventory
    {
        private class Item
        {
            public Product product;
            public int quantity;

            // Must intialize non-empty item
            public Item(Product p, int q)
            {
                product = p;
                quantity = q;
            }
        }

        /// <summary>
        /// Invetories are initiazlied with an empty list
        /// List items comprise of a product and a corresponding quantity
        /// </summary>
        //private List<Tuple<Product, int>> _myList = new List<Tuple<Product, int>>();

        private List<Item> _myList = new List<Item>();



        /// <summary>
        /// Checks if given product list contains a product with given name
        /// </summary>
        /// <param name="name">Name of product to be searched for</param>
        /// <param name="pl">Product list to be searched</param>
        /// <returns>True if contains, False if not</returns>
        public bool Contains(Product product)
        {
            // Return false if GetItem returns null
            Item listItem = GetItem(product);
            if (listItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        ///// <summary>
        ///// Checks if given product list contains a product with given name
        ///// </summary>
        ///// <param name="name">Name of product to be searched for</param>
        ///// <param name="pl">Product list to be searched</param>
        ///// <returns>True if contains, False if not</returns>
        //public bool Contains(Product product, out Product foundProduct)
        //{
        //    // If GetItem returns something, out the result's Product and return true
        //    Tuple<Product, int> listItem = GetItem(product);
        //    foundProduct = listItem.Item1;
        //    if (foundProduct!= null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Returns a list item in the inventory matching with given product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Returns the list item containing the given product; Null if not found</returns>
        //private Tuple<Product, int> GetItem(Product product);
        private Item GetItem(Product product)
        {
            foreach (Item listItem in _myList)
            {
                // If there is a matching product in list, return
                Product currProduct = listItem.product;
                if (currProduct.Name == product.Name && currProduct.Price == product.Price
                    && currProduct.Size == product.Size && currProduct.Color == product.Color)
                {
                    return listItem;
                }
            }
            return null;
        }

        /// <summary>
        /// 
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

            // If inventory already has particular product, fetch that product and 
            // If inventory doesn't already have product, add it to list
            Item listItem = GetItem(product);
            if (listItem == null)
            {
                listItem = new Item(product,quantity);
                _myList.Add(listItem);
            }
            // If inventory already has product, fetch corresponding list item and increase quantity
            else
            {
                listItem.quantity += quantity;
                _myList.Add(listItem);
            }
        }
    }
}
