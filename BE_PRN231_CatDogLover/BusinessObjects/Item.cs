using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Item
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Products = new HashSet<Product>();
            ServiceSchedulers = new HashSet<ServiceScheduler>();
        }

        public string ItemId { get; set; } = null!;
        public string ItemType { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ServiceScheduler> ServiceSchedulers { get; set; }
    }
}
