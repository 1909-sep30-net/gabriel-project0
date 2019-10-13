using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Entities
{
    public partial class Products
    {
        public Products()
        {
            InventoryItems = new HashSet<InventoryItems>();
            OrderItems = new HashSet<OrderItems>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? ColorId { get; set; }
        public decimal Price { get; set; }

        public virtual Colors Color { get; set; }
        public virtual ICollection<InventoryItems> InventoryItems { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
