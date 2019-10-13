using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Entities
{
    public partial class InventoryItems
    {
        public int InventoryItemId { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Products Product { get; set; }
    }
}
