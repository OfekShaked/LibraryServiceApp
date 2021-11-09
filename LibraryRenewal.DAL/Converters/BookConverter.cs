using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.DAL.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryRenewal.DAL.Converters
{
    public class BookConverter : IBookConverter
    {
        ILibraryDbContext context;
        IGenreConverter genreConverter;
        public BookConverter(ILibraryDbContext context)
        {
            this.context = context;
            this.genreConverter = new GenreConverter();
        }
        public Book BookDTOToBook(Models.AbstractItem book)
        {
            Book bookToSend = new Book
            {
                Genre = genreConverter.GenreDTOToGenre(context.Genres.FirstOrDefault(x => x.GenreId == book.IdofGenre)),
                Discount = (int)book.Discount,
                Edition=book.Edition,
                ISBN=book.Isbn,
                ItemID= (int)book.ItemId,
                Name=book.Name,
                Price= (int)book.Price,
                PrintDate= DateTime.Parse(book.PrintDate),
                Publisher=book.Publisher,
                Quantity= (int)book.Quantity,
                Summary=book.Summary,
                Writer=book.Writer
            };
            return bookToSend;
        }
        Models.AbstractItem IBookConverter.BookToBookDTO(Book book)
        {
            return new Models.AbstractItem
            {
                Discount=book.Discount,
                Edition=book.Edition,
                IdofGenre=book.Genre.ID,
                Isbn=book.ISBN,
                ItemId=book.ItemID,
                Name=book.Name,
                Price=book.Price,
                PrintDate=book.PrintDate.ToString(),
                Publisher=book.Publisher,
                Quantity=book.Quantity,
                Summary=book.Summary,
                Writer=book.Writer
            };
        }
    }
}
