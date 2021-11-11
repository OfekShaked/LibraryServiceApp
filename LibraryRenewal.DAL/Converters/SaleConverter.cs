using LibraryRenewal.DAL.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Converters
{
    public class SaleConverter : ISaleConverter
    {
        public Models.Sale SaleToSaleDTO(Common.Models.Sale sale)
        {
            if (sale == null) return null;
            return new Models.Sale
            {
                SaleDate= sale.DateSold.ToString(),
                ItemSoldId=sale.ItemSoldID,
                QuantitySold=sale.QuantitySold,
                SaleId=sale.SaleID,
                SalesManUsername=sale.SalesManUsername
            };
        }
        public Common.Models.Sale SaleDTOToSale(Models.Sale sale)
        {
            if (sale == null) return null;
            return new Common.Models.Sale
            {
                DateSold= DateTime.Parse(sale.SaleDate),
                ItemSoldID= (int)sale.ItemSoldId,
                QuantitySold= (int)sale.QuantitySold,
                SaleID= (int)sale.SaleId,
                SalesManUsername=sale.SalesManUsername
            };
        }
    }
}
