using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
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
                InventoryItems = model.Inventory.Select(i => MapInventoryItem(model,i)).ToList()
            };

            return result;
        }

        public static Library.Location MapLocation(Entities.Locations dbmodel)
        {
            Library.Location result = new Library.Location
            {
                Id = dbmodel.LocationId,
                Name = dbmodel.Name,
                Inventory = dbmodel.InventoryItems.Select(MapInventoryItem).ToList()
            };

            return result;
        }

        /*------------------------------------------------------*/


        /* Order mapping */

        public static Entities.Orders MapOrder(Library.Order model)
        {
            Entities.Orders result = new Entities.Orders
            {
                //OrderId = model.OrderID,
                CustomerId = model.MyCustomer.CustomerId,
                LocationId = model.MyLocation.Id,

                TimeConfirmed = model.MyTime,
                OrderItems = model.ProductList.Select(i=>MapOrderItem(i,model)).ToList()
            };

            return result;
        }

        public static Library.Order MapOrder(Entities.Orders dbmodel)
        {
            Library.Order result = new Library.Order
            {
                OrderID = dbmodel.OrderId,
                MyCustomer = MapCustomer(dbmodel.Customer),                
                MyLocation = MapLocation(dbmodel.Location),

                MyTime = dbmodel.TimeConfirmed,
                ProductList = dbmodel.OrderItems.Select(MapOrderItem).ToList(),
            };

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
                //ColorId = model.ColorID
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
                //ColorID = dbmodel.ColorId ?? 0
            };

            return result;
        }

        /*------------------------------------------------------*/



        /* Item to InventoryItems Mapping */

        public static Entities.InventoryItems MapInventoryItem(Library.Location modelL, Library.Item modelP)
        {
            Entities.InventoryItems result = new Entities.InventoryItems
            {
                Product = MapProduct(modelP.Product),
                ProductId = modelP.Product.ID,
                Quantity = modelP.Quantity,
                LocationId = modelL.Id,
                Location = MapLocation(modelL)
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
        public static Entities.OrderItems MapOrderItem(Library.Item modelI, Library.Order modelO)
        {
            Entities.OrderItems result = new Entities.OrderItems
            {
                OrderId = modelO.OrderID,
                ProductId = modelI.Product.ID,
                Quantity = modelI.Quantity,
            };

            return result;
        }


        public static Library.Item MapOrderItem(Entities.OrderItems dbmodel)
        {
            Library.Item result = new Library.Item
            {
                // Should this be changed?
                //ItemID = dbmodel.OrderItem,
                Product = MapProduct(dbmodel.Product),
                Quantity = dbmodel.Quantity
            };

            return result;
        }

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


        /* ------------------------------------------------------------------- */

    }
}
