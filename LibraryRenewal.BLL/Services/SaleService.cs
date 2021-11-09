using LibraryRenewal.BLL.Interfaces;
using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Services
{
    public class SaleService : ISaleService
    {
        ISaleRepository _saleRep;
        public SaleService(ISaleRepository saleRep)
        {
            _saleRep = saleRep;
        }
        public async Task AddNewSale(string salesManUsername, int itemSoldID, int quantitySold)
        {
            Sale s1 = new Sale();
            s1.SalesManUsername = salesManUsername;
            s1.ItemSoldID = itemSoldID;
            s1.QuantitySold = quantitySold;
            await _saleRep.AddNewSale(s1);
        }
    }
}
