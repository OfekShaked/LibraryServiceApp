using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Testing.Mocks
{
    class MockGeneralRepository : IGeneralRepository
    {
        List<string> logFile = new List<string>();
        static int itemID = 1;
        MockBookRepository bookRep;
        MockJournalRepository journalRep;
        public MockGeneralRepository(MockBookRepository _bookRep, MockJournalRepository _journalRep)
        {
            bookRep = _bookRep;
            journalRep = _journalRep;
        }
        public static int GetID()
        {
            return itemID++;
        }
        public void SaveToLogFile(string error)
        {
            logFile.Add(error);
        }

        public async Task<AbstractItem> GetItemByID(int id)
        {
            Book b1 = await bookRep.GetBook(id);
            if (b1 != null)
                return b1;
            Journal j1 = await journalRep.GetJournal(id);
            if (j1 != null)
                return j1;
            return null;

        }

        public async Task UpdateAbstractItem(int id, AbstractItem updatedItem)
        {
            Book b1 = await bookRep.GetBook(id);
            if (b1 != null)
            {
                b1.Name = updatedItem.Name;
                b1.Writer = updatedItem.Writer;
                b1.PrintDate = updatedItem.PrintDate;
                b1.Publisher = updatedItem.Publisher;
                b1.Genre = updatedItem.Genre;
                b1.Discount = updatedItem.Discount;
                b1.Quantity = updatedItem.Quantity;
                await bookRep.UpdateBook(id, b1);
            }
            Journal j1 = await journalRep.GetJournal(id);
            if (j1 != null)
            {
                j1.Name = updatedItem.Name;
                j1.Writer = updatedItem.Writer;
                j1.PrintDate = updatedItem.PrintDate;
                j1.Publisher = updatedItem.Publisher;
                j1.Genre = updatedItem.Genre;
                j1.Discount = updatedItem.Discount;
                j1.Quantity = updatedItem.Quantity;
                await journalRep.UpdateJournal(id, j1);
            }
        }
    }
}
