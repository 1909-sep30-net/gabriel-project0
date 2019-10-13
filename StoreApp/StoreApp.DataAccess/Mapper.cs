using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DataAccess
{
    /// <summary>
    /// Maps all DB Entities to the domain model objects
    /// </summary>
    public static class Mapper
    {

        /* Customer mapping */

        public static Entities.Customers MapCustomer(Library.Customer model)
        {
            Entities.Customers result = new Entities.Customers
            {
                CustomerId = model.CustomerId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Orders = MapOrderLog(model.OrderLog)

            };

            return result;
        }
        public static Library.Customer MapCustomer(Entities.Customers dbmodel)
        {
            Library.Customer result = new Library.Customer
            {
                CustomerId = dbmodel.CustomerId,
                FirstName = dbmodel.FirstName,
                LastName = dbmodel.LastName,
                OrderLog = MapOrderLog(dbmodel.Orders)// ADD ORDERS
            };

            return result;
        }

        /*------------------------------------------------------*/


        /* Location mapping */

        public static Entities.Locations MapLocation(Library.Location model)
        {
            Entities.Locations result = new Entities.Locations
            {
                LocationId = model.Id,
                Name = model.Name,
                Orders = MapOrderLog(model.OrderLog),
                InventoryItems = MapInventory(model)
            };

            return result;
        }

        public static Library.Location MapLocation(Entities.Locations dbmodel)
        {
            Library.Location result = new Library.Location
            {
                Id = dbmodel.LocationId,
                Name = dbmodel.Name,
                OrderLog = MapOrderLog(dbmodel.Orders),
                Inventory = MapInventory(dbmodel.InventoryItems)
            };

            return result;
        }

        /*------------------------------------------------------*/


        /* Order mapping */

        public static Entities.Orders MapOrder(Library.Order model)
        {
            Entities.Orders result = new Entities.Orders
            {
                Customer = MapCustomer(model.MyCustomer),
                CustomerId = model.MyCustomer.CustomerId,

                Location = MapLocation(model.MyLocation),
                LocationId = model.MyLocation.Id,

                OrderItems = MapInventory(model),
                TimeConfirmed = model.MyTime
            };

            return result;
        }

        public static Library.Order MapOrder(Entities.Orders dbmodel)
        {
            Library.Order result = new Library.Order
            {
                MyCustomer = MapCustomer(dbmodel.Customer),
                MyLocation = MapLocation(dbmodel.Location),
                MyTime = dbmodel.TimeConfirmed,

                ProductList = MapInventory(dbmodel.OrderItems)
            };

            return result;
        }

        /*------------------------------------------------------*/


        /* OrderList mapping -----------------------------------*/

        public static List<Library.Order> MapOrderLog( ICollection<Entities.Orders> dbmodel)
        {
            List<Library.Order> result = new List<Library.Order>();
            foreach( Entities.Orders order in dbmodel)
            {
                result.Add(MapOrder(order));
            }
            return result;
        }

        public static ICollection<Entities.Orders> MapOrderLog(List<Library.Order> model)
        {
            List<Entities.Orders> result = new List<Entities.Orders>();
            foreach (Library.Order order in model)
            {
                result.Add(MapOrder(order));
            }
            return result;
        }

        /*------------------------------------------------------*/

        /* Product mapping */

        public static Entities.Products MapProduct(Library.Product model)
        {
            Entities.Products result = new Entities.Products
            {
                ProductId = model.ID,
                Name = model.Name,
                Price = model.Price,
                ColorId = model.ColorID
            };

            return result;
        }

        public static Library.Product MapProduct(Entities.Products dbmodel)
        {
            Library.Product result = new Library.Product
            {
                ID = dbmodel.ProductId,
                Price = dbmodel.Price,
                Name = dbmodel.Name,
                ColorID = dbmodel.ColorId ?? 0
            };

            return result;
        }

        /*------------------------------------------------------*/

        /* Color mapping */

        public static Entities.Colors MapColor(Library.Color model)
        {
            Entities.Colors result = new Entities.Colors
            {
                ColorId = model.Color_id,
                Color = model.Name
            };

            return result;
        }

        public static Library.Color MapColor(Entities.Colors dbmodel)
        {
            Library.Color result = new Library.Color
            {
                Color_id = dbmodel.ColorId,
                Name = dbmodel.Color
            };

            return result;
        }

        /* ------------------------------------------------------------ */


        /* Inventory to InventoryItems List Mapping */

        public static List<Library.Item> MapInventory(ICollection<Entities.InventoryItems> dbmodel)
        {

            List<Library.Item> result= new List<Library.Item>();
            foreach(Entities.InventoryItems item in dbmodel)
            {
                result.Add(MapInventoryItem(item));
            }

            return result;

        }

        public static List<Entities.InventoryItems> MapInventory(Library.Location modelL)
        {
            List<Entities.InventoryItems> result = new List<Entities.InventoryItems>();
            foreach (Library.Item item in modelL.Inventory)
            {
                result.Add(MapInventoryItem(modelL, item));
            }
            return result;
        }

        /* ---------------------------------------------------------------- */

        /* ProductList to OrderItems List Mapping */

        public static List<Library.Item> MapInventory(ICollection<Entities.OrderItems> dbmodel)
        {

            List<Library.Item> result = new List<Library.Item>();
            foreach (Entities.OrderItems item in dbmodel)
            {
                result.Add(MapInventoryItem(item));
            }

            return result;

        }

        public static List<Entities.OrderItems> MapInventory(Library.Order modelO)
        {
            List<Entities.OrderItems> result = new List<Entities.OrderItems>();
            foreach (Library.Item item in modelO.ProductList)
            {
                result.Add(MapInventoryItem(modelO, item));
            }
            return result;
        }


        /* ------------------------------------------------------------------- */

        /* Item to InventoryItems Mapping */

        public static Entities.InventoryItems MapInventoryItem(Library.Location modelL, Library.Item modelP)
        {
            Entities.InventoryItems result = new Entities.InventoryItems
            {
                Product = MapProduct(modelP.Product),
                ProductId = modelP.Product.ID,
                Quantity = modelP.Quantity,
                LocationId = modelL.Id,
                Location = MapLocation(modelL),
            };

            return result;
        }
        public static Library.Item MapInventoryItem(Entities.InventoryItems dbmodel)
        {
            Library.Item result = new Library.Item
            {
                Product = MapProduct(dbmodel.Product),
                Quantity = dbmodel.Quantity
            };

            return result;
        }

        /* --------------------------------------------------------- */

        /* Item to OrderItems Mapping */
        public static Entities.OrderItems MapInventoryItem(Library.Order modelO, Library.Item modelI)
        {
            Entities.OrderItems result = new Entities.OrderItems
            {
                Product = MapProduct(modelI.Product),
                ProductId = modelI.Product.ID,
                Quantity = modelI.Quantity,
                Order = MapOrder(modelO),
                
            };

            return result;
        }


        public static Library.Item MapInventoryItem(Entities.OrderItems dbmodel)
        {
            Library.Item result = new Library.Item
            {
                Product = MapProduct(dbmodel.Product),
                Quantity = dbmodel.Quantity
            };

            return result;
        }

    }
}
