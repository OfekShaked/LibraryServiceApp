using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Interfaces.Converters
{
    public interface ISaleConverter
    {
        Models.Sale SaleToSaleDTO(Common.Models.Sale sale);
        Common.Models.Sale SaleDTOToSale(Models.Sale sale);
    }
}
