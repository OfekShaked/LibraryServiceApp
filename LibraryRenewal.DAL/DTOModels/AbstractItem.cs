using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Models
{
    public partial class AbstractItem
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Writer { get; set; }
        public string PrintDate { get; set; }
        public string Publisher { get; set; }
        public long IdofGenre { get; set; }
        public long Discount { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public string Isbn { get; set; }
        public string Edition { get; set; }
        public string Summary { get; set; }
        public string Subject { get; set; }
    }
}
