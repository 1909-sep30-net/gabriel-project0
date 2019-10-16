using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.DataAccess.Repositories
{
    public class LocationRepository
    {
        private DoapSoapContext dbcontext;

        public LocationRepository(DoapSoapContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context cant be null.", nameof(context));
            }
            dbcontext = context;
        }

        public IEnumerable<Library.Location> GetLocations()
        {
            return dbcontext.Locations.Select(Mapper.MapLocation).ToList();
        }

        /// <summary>
        /// Get's a location's order history
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Library.Order> GetOrders(int id)
        {
            return dbcontext.Orders
                .Where(o => o.LocationId == id)
                .Include(o => o.Customer)
                    .ThenInclude(oi=>oi.Orders)
                .Select(Mapper.MapOrder)
                .ToList();
        }

        /// <summary>
        /// Given a location id, return a list of location's inventory items (with product)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Library.Item> GetInventoryItemsById(int id)
        {
            return dbcontext.InventoryItems
                .Where(ii => ii.LocationId == id)
                .Include(ii => ii.Product)
                .Select(Mapper.MapInventoryItem)
                .ToList();
        }

        /// <summary>
        /// Find a mapped location by ID if found in db, otherwise return null
        /// </summary>
        /// <param name="id">ID of location to be found</param>
        /// <returns>Returns a mapped location if found in db, otherwise return null</returns>
        public Library.Location GetLocationByID(int id)
        {
            return Mapper.MapLocation(dbcontext.Locations.Find(id)) ?? null;
        }

        /// <summary>
        /// Given a business model inventory, update inverntory items in database
        /// </summary>
        /// <param name="items"></param>
        public void UpdateInventory(IEnumerable<Library.Item> blInventory, Library.Location blLocation)
        {
            // populate list with mapped items
            foreach(Library.Item item in blInventory)
            {
                // find associated inventory item using item id
                var newEntity = Mapper.MapInventoryItem(blLocation, item);
                var oldEntity = dbcontext.InventoryItems.Where(ii => ii.LocationId == newEntity.LocationId && ii.ProductId == newEntity.ProductId).FirstOrDefault();
                oldEntity.Quantity = newEntity.Quantity;
            }
        }

        public void DisplayItems()
        {
            List<Entities.InventoryItems> list = dbcontext.InventoryItems.ToList();
            //dbcontext.InventoryItems
            Console.WriteLine(list.Select(x => x.LocationId));
        }
    }
}
