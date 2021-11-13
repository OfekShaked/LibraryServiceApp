using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces;
using LibraryRenewal.BLL.Interfaces.Validations;
using LibraryRenewal.BLL.Validations;
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
    public class BookService : IBookService
    {
        IBookRepository _bookRep;
        IGenreRepository _genreRep;
        ISaleService _saleService;
        IBookValidation _bookValidation;
        IGeneralValidations _generalValidations;
        public BookService(IBookRepository bookRep, IGenreRepository genreRep, ISaleService saleService, IBookValidation bookValidations, IGeneralValidations generalValidations)
        {
            _genreRep = genreRep;
            _bookRep = bookRep;
            _saleService = saleService;
            _bookValidation = bookValidations;
            _generalValidations = generalValidations;
        }
        public async Task AddNewBook(string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string isbn, string edition, string summary)
        {
            try
            {
                Book b1 = new Book();
                int discountToAdd, quantityToAdd, priceToAdd;
                var isValid = _bookValidation.IsBookValid(name, writer, printdate, publisher, genre, discount, quantity, price, isbn, edition, summary, out discountToAdd, out quantityToAdd, out priceToAdd,out DateTime date);
                b1.Name = name;
                b1.Writer = writer;
                b1.PrintDate = DateTime.Parse(printdate);
                b1.Publisher = publisher;
                Genre g1 = await _genreRep.GetGenre(genre);
                if (g1 == null)
                {
                    g1 = new Genre(genre);
                    await _genreRep.AddGenre(g1);
                }
                b1.Genre = g1;
                b1.Discount = discountToAdd;
                b1.Quantity = quantityToAdd;
                b1.Price = priceToAdd;
                List<string> isbns = await _bookRep.GetAllIsbn();
                if (isbns.Contains(isbn))
                {
                    await GeneralLibraryLogic.SaveToLogFile("ISBN Already Exist!");
                    throw new BLLBookException("ISBN Already Exist!");
                }
                b1.ISBN = isbn;
                b1.Edition = edition;
                b1.Summary = summary;

                await _bookRep.AddBook(b1);
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot add a new book atm try again later or call a manager");
            }
        }

        public async Task DeleteBook(string itemID)
        {
            try
            {
                int id = _generalValidations.IsItemIdValid(itemID);
                Book b1 = await _bookRep.GetBook(id);
                await _bookRep.DeleteBook(b1);
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot delete a atm try again later or call a manager");
            }
        }
        /// <summary>
        /// Get a list of all the books with quantity of more than 1
        /// </summary>
        /// <returns></returns>
        public async Task<List<Book>> GetAllAvailableBooks()
        {
            try
            {
                List<Book> books = await _bookRep.GetAllBooks();
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].Quantity == 0)
                    {
                        books.RemoveAt(i);
                    }
                }
                return books;
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot get all books atm try again later or call a manager");
                return new List<Book>();
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                return await _bookRep.GetAllBooks();
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot get all books atm try again later or call a manager");
                return new List<Book>();
            }
        }
        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<Book> GetBook(string itemID)
        {
            int id = _generalValidations.IsItemIdValid(itemID);
            try
            {
                return await _bookRep.GetBook(id);
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot update a book atm try again later or call a manager");
                return null;
            }
        }
        /// <summary>
        /// Remove 1 from the quantity of books if book quantity is 1 or more
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task SellBook(string itemID, string username)
        {
            try
            {
                int id = _generalValidations.IsItemIdValid(itemID);
                Book b1 = await GetBook(itemID);
                if (b1.Quantity > 0)
                {
                    b1.Quantity -= 1;
                }
                else
                {
                    await GeneralLibraryLogic.SaveToLogFile("Cannot sell a book out of stock");
                    throw new BLLBookException("Cannot sell a book out of stock");
                }

                await UpdateBook(itemID, b1.Name, b1.Writer, b1.PrintDate.ToString(), b1.Publisher, b1.Genre.Name, b1.Discount.ToString(), b1.Quantity.ToString(), b1.Price.ToString(), b1.ISBN, b1.Edition, b1.Summary);
                await _saleService.AddNewSale(username, id, 1);
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot sell a book atm try again later or call a manager");
            }
        }
        public async Task UpdateBook(string idToUpdate, string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string isbn, string edition, string summary)
        {
            try
            {
                int discountToAdd, quantityToAdd, priceToAdd;
                var isValid = _bookValidation.IsBookValid(name, writer, printdate, publisher, genre, discount, quantity, price, isbn, edition, summary, out discountToAdd, out quantityToAdd, out priceToAdd,out DateTime date);
                int id = _generalValidations.IsItemIdValid(idToUpdate);
                Book b1 = new Book();
                b1.ItemID = id; 
                b1.Name = name;
                b1.Writer = writer;
                b1.PrintDate = DateTime.Parse(printdate);
                b1.Publisher = publisher;
                Genre g1 = await _genreRep.GetGenre(genre);
                if (g1.Name == null)
                {
                    g1.Name = genre;
                    await _genreRep.AddGenre(g1);
                }
                b1.Genre = g1;
                b1.Discount = discountToAdd;
                b1.Quantity = quantityToAdd;
                b1.Price = priceToAdd;
                b1.ISBN = isbn;
                b1.Edition = edition;
                b1.Summary = summary;
                await _bookRep.UpdateBook(id, b1);
            }
            catch (Exception e)
            {
                await CatchError(e, "Cannot update a book atm try again later or call a manager");
            }
        }

        private static async Task CatchError(Exception e, string message)
        {
            if (e is BLLBookException
                                 || e is DALException)
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new BLLBookException(message);
            }
            else
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new LibraryException("Unknown error inform a manager!");
            }
        }
    }
}
