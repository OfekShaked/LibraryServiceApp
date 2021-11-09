using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Common.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public string SalesManUsername { get; set; }
        public int ItemSoldID { get; set; }
        public DateTime DateSold { get; set; }
        public int QuantitySold { get; set; }
        public Sale()
        {
            DateSold = DateTime.Now;
        }

    }
}
