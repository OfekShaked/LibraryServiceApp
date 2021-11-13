using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces;
using LibraryRenewal.BLL.Interfaces.Validations;
using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Exceptions;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Services
{
    public class JournalService : IJournalService
    {
        IJournalRepository _journalRep;
        IGenreRepository _genreRep;
        ISaleService _saleService;
        IGeneralValidations _generalValidations;
        IJournalValidations _journalValidations;
        public JournalService(IJournalRepository journalRep, IGenreRepository genreRep, ISaleService saleService, IGeneralValidations generalValidations, IJournalValidations journalValidations)
        {
            _genreRep = genreRep;
            _journalRep = journalRep;
            _saleService = saleService;
            _generalValidations = generalValidations;
            _journalValidations = journalValidations;
        }
        public async Task AddNewJournal(string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string subject)
        {
            try
            {
                Journal j1 = new Journal();
                var isValid = _journalValidations.IsJournalValid(name, writer, printdate, publisher, genre, discount, quantity, price, subject, out int discountToAdd, out int quantityToAdd, out int priceToAdd, out DateTime date);
                j1.Name = name;
                j1.Writer = writer;
                j1.PrintDate = date;
                j1.Publisher = publisher;

                Genre g1 = await _genreRep.GetGenre(genre);
                if (g1 == null)
                {
                    g1 = new Genre(genre);
                    await _genreRep.AddGenre(g1);
                    g1 = await _genreRep.GetGenre(genre);
                }
                j1.Genre = g1;
                j1.Discount = discountToAdd;
                j1.Quantity = quantityToAdd;
                j1.Price = priceToAdd;
                j1.Subject = subject;

                await _journalRep.AddJournal(j1);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot add a new journal atm try again later or call a manager");
            }
        }

        public async Task DeleteJournal(string itemID)
        {
            try
            {
                int id = _generalValidations.IsItemIdValid(itemID);
                Journal j1 = await _journalRep.GetJournal(id);
                await _journalRep.DeleteJournal(j1);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot delete a journal atm try again later or call a manager");
            }
        }

        public async Task<List<Journal>> GetAllAvailableJournals()
        {
            try
            {
                List<Journal> journals = await _journalRep.GetAllJournals();
                for (int i = 0; i < journals.Count; i++)
                {
                    if (journals[i].Quantity == 0)
                    {
                        journals.RemoveAt(i);
                    }
                }
                return journals;
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot get all available journals atm try again later or call a manager");
                return new List<Journal>();
            }
        }

        public async Task<List<Journal>> GetAllJournals()
        {
            try
            {
                return await _journalRep.GetAllJournals();
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot get all journals atm try again later or call a manager");
                return new List<Journal>();
            }
        }
        /// <summary>
        /// Get a journal by journal id
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns>Journal found or null</returns>
        public async Task<Journal> GetJournal(string itemID)
        {
            int id = _generalValidations.IsItemIdValid(itemID);
            try
            {
                return await _journalRep.GetJournal(id);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot get a journal atm try again later or call a manager");
                return null;
            }
        }

        public async Task UpdateJournal(string idToUpdate, string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string subject)
        {
            try
            {
                int id = _generalValidations.IsItemIdValid(idToUpdate);
                Journal j1 = new Journal();
                var isValid = _journalValidations.IsJournalValid(name, writer, printdate, publisher, genre, discount, quantity, price, subject, out int discountToAdd, out int quantityToAdd, out int priceToAdd, out DateTime date);
                j1.ItemID = id;
                j1.Name = name;
                j1.Writer = writer;
                j1.PrintDate = date;
                j1.Publisher = publisher;
                Genre g1 = await _genreRep.GetGenre(genre);
                if (g1.Name == null)
                {
                    g1.Name = genre;
                    await _genreRep.AddGenre(g1);
                }
                j1.Genre = g1;
                j1.Discount = discountToAdd;
                j1.Quantity = quantityToAdd;
                j1.Price = priceToAdd;
                j1.Subject = subject;
                await _journalRep.UpdateJournal(id, j1);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot update a journal atm try again later or call a manager");
            }
        }
        /// <summary>
        /// Removes 1 from the quantity of the journal if the quantity is 1 or more
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task SellJournal(string itemID, string username)
        {
            try
            {
                int id = _generalValidations.IsItemIdValid(itemID);
                Journal j1 = await GetJournal(itemID);
                if (j1.Quantity > 0)
                {
                    j1.Quantity -= 1;
                }
                await UpdateJournal(itemID, j1.Name, j1.Writer, j1.PrintDate.ToString(), j1.Publisher, j1.Genre.Name, j1.Discount.ToString(), j1.Quantity.ToString(), j1.Price.ToString(), j1.Subject);
                await _saleService.AddNewSale(username, id, 1);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot sell a journal atm try again later or call a manager");
            }
        }

        private static async Task CatchException(Exception e,string message)
        {
            if (e is BLLJournalException
                                 || e is DALException)
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new BLLJournalException("Cannot sell a journal atm try again later or call a manager");
            }
            else
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new LibraryException("Unknown error inform a manager!");
            }
        }
    }
}
