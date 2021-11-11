using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Exceptions;
using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.DAL.Interfaces.Converters;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        IGenreRepository _genreRep;
        ILibraryDbContext _context;
        IBookConverter _bookConverter;
        public BookRepository(IGenreRepository genreRep, ILibraryDbContext context, IBookConverter bookConverter)
        {
            _genreRep = genreRep;
            _context = context;
            _bookConverter = bookConverter;
        }
        public async Task<List<Book>> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                var items = _context.AbstractItems.Where(x => x.Subject == null).ToList();
                foreach (var item in items)
                {
                    books.Add(_bookConverter.BookDTOToBook(item));
                }
                return books;
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new BookException("Cant get all books");
            }

        }
        public async Task<Book> GetBook(int itemID)
        {
            try
            {
                var bookFound = _context.AbstractItems.FirstOrDefault(x => x.ItemId == itemID && x.Subject == null);
                return _bookConverter.BookDTOToBook(bookFound);
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new BookException("Cant get book");
            }
        }
        public async Task AddBook(Book book)
        {
            try
            {
                _context.AbstractItems.Add(_bookConverter.BookToBookDTO(book));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new BookException("Cant add book");
            }
        }
        public async Task DeleteBook(Book book)
        {
            try
            {
                _context.AbstractItems.Remove(_context.AbstractItems.FirstOrDefault(x => x.ItemId == book.ItemID));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new BookException("Cant Delete book");
            }
        }

        public async Task UpdateBook(int id, Book updatedBook)
        {
            try
            {
                updatedBook.ItemID = id;
                var book = _bookConverter.BookToBookDTO(updatedBook);
                var bookFromRep = _context.AbstractItems.FirstOrDefault(x => x.ItemId == id);
                bookFromRep.Quantity = book.Quantity;
                bookFromRep.Discount = book.Discount;
                bookFromRep.Price = book.Price;
                bookFromRep.Edition = book.Edition;
                bookFromRep.IdofGenre = book.IdofGenre;
                bookFromRep.Isbn = book.Isbn;
                bookFromRep.Name = book.Name;
                bookFromRep.PrintDate = book.PrintDate;
                bookFromRep.Publisher = book.Publisher;
                bookFromRep.Summary = book.Summary;
                bookFromRep.Subject = book.Subject;
                bookFromRep.Writer = book.Writer; 
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new BookException("Cant Update book");
            }
        }
        public async Task<List<string>> GetAllIsbn()
        {
            try
            {
                return _context.AbstractItems.Where(x => x.Isbn != null).Select(x => x.Isbn).ToList();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new BookException("Cant get isbns");
            }
        }


    }
}
