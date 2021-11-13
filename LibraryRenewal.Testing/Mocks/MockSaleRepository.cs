using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Testing.Mocks
{
    class MockSaleRepository : ISaleRepository
    {
        List<Sale> sales;
        public MockSaleRepository()
        {
            sales = new List<Sale>();
        }
        public async Task AddNewSale(Sale s1)
        {
            sales.Add(s1);
        }
    }
}
