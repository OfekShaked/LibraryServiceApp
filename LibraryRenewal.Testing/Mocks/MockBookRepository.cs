using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Testing.Mocks
{
    class MockBookRepository : IBookRepository
    {
        List<Book> books = new List<Book>();
        public async Task AddBook(Book b1)
        {
            b1.ItemID = MockGeneralRepository.GetID();
            books.Add(b1);
        }

        public async Task DeleteBook(Book b1)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ItemID.Equals(b1.ItemID))
                {
                    books.RemoveAt(i);
                    return;
                }
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return books;
        }

        public async Task<List<string>> GetAllIsbn()
        {
            List<string> isbns = new List<string>();
            for (int i = 0; i < books.Count; i++)
            {
                isbns.Add(books[i].ISBN);
            }
            return isbns;
        }

        public async Task<Book> GetBook(int id)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ItemID.Equals(id))
                {
                    return books[i];
                }
            }
            return null;
        }

        public async Task UpdateBook(int id, Book updatedBook)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ItemID.Equals(id))
                {
                    updatedBook.ItemID = books[i].ItemID;
                    books[i] = updatedBook;
                }
            }
        }
    }
}
