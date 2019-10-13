using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Entities
{
    public partial class Locations
    {
        public Locations()
        {
            InventoryItems = new HashSet<InventoryItems>();
            Orders = new HashSet<Orders>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InventoryItems> InventoryItems { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
