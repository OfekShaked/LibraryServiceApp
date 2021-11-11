using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.DAL.Interfaces.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryRenewal.DAL.Converters
{
    public class AbstractItemConverter : IAbstractItemConverter
    {
        ILibraryDbContext context;
        IGenreConverter genreConverter;
        public AbstractItemConverter(ILibraryDbContext context,IGenreConverter genreConverter)
        {
            this.context = context;
            this.genreConverter = genreConverter;
        }
        public Common.Models.AbstractItem AbstractItemDTOToAbstractItem(Models.AbstractItem abstractItem)
        {
            if (abstractItem == null) return null;
            return new Common.Models.Book
            {
                Discount = (int)abstractItem.Discount,
                Edition = abstractItem.Edition,
                Genre = genreConverter.GenreDTOToGenre(context.Genres.FirstOrDefault(x => x.GenreId == abstractItem.IdofGenre)),
                ItemID= (int)abstractItem.ItemId,
                Name=abstractItem.Name,
                Price= (int)abstractItem.Price,
                PrintDate=DateTime.Parse(abstractItem.PrintDate),
                ISBN=abstractItem.Isbn,
                Publisher=abstractItem.Publisher,
                Quantity= (int)abstractItem.Quantity,
                Summary=abstractItem.Summary,
                Writer=abstractItem.Writer
            };
        }

        public Models.AbstractItem AbstractItemToAbstractItemDTO(Common.Models.AbstractItem abstractItem)
        {
            if (abstractItem == null) return null;

            return new Models.AbstractItem
            {
                Discount = abstractItem.Discount,
                IdofGenre = abstractItem.Genre.ID,
                ItemId = abstractItem.ItemID,
                Name = abstractItem.Name,
                Price = abstractItem.Price,
                PrintDate = abstractItem.PrintDate.ToString(),
                Quantity = abstractItem.Quantity,
            };
        }
    }
}
