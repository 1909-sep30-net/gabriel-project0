using System;
using System.Collections.Generic;

namespace StoreApp.DataAccess.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime TimeConfirmed { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Locations Location { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
