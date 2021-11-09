using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Models
{
    public partial class Sale
    {
        public long SaleId { get; set; }
        public string SalesManUsername { get; set; }
        public long ItemSoldId { get; set; }
        public string SaleDate { get; set; }
        public long QuantitySold { get; set; }
    }
}
