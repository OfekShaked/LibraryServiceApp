using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Exceptions;
using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.DAL.Interfaces.Converters;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        ILibraryDbContext _context;
        ISaleConverter _converter;
        public SaleRepository(ILibraryDbContext context, ISaleConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public async Task AddNewSale(Sale sale)
        {
            try
            {
                _context.Sales.Add(_converter.SaleToSaleDTO(sale));
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new SaleException("Cant add a new sale");
            }
        }

    }
}
