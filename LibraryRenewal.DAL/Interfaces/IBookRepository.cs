using LibraryRenewal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBook(int id);
        Task AddBook(Book b1);
        Task DeleteBook(Book b1);
        Task UpdateBook(int id, Book updatedBook);
        Task<List<string>> GetAllIsbn();
    }
}
