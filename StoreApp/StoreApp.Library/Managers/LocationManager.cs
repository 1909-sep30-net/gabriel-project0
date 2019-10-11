using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Managers
{
    public static class LocationManager
    {
        /// <summary>
        /// Verifies if this location instance is valid
        /// </summary>
        /// <remarks>
        /// - Inventory must not be empty list
        /// - Name must be non-null, non-empty string
        /// </remarks>
        /// <returns></returns>
        public static bool IsValidLocation(Location location)
        {
            if (location.Inventory.Count < 1 )
            {
                return false;
            }

            if (string.IsNullOrEmpty(location.Name))
            {
                return false;
            }

            return true;
        }

        public static void AddToInventory(Location location, Product product, int quantity)
        {
            // location.Inventory()
        }

        public static void RemoveFromInventory()
        {

        }

        public static bool InventoryContains(Location location, string name)
        {
            foreach (Tuple<Product,int> item in location.Inventory)
            {
                if (item.Item1.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
