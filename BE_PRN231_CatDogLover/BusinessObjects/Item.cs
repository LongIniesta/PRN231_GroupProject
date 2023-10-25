using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Item
    {
        public string ItemId { get; set; } = null!;
        public string ItemType { get; set; } = null!;

        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ServiceScheduler? ServiceScheduler { get; set; }
    }
}
