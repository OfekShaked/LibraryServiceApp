using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.DAL.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryRenewal.DAL.Converters
{
    public class JournalConverter : IJournalConverter
    {
        ILibraryDbContext context;
        private IGenreConverter genreConverter;

        public JournalConverter(ILibraryDbContext context)
        {
            this.context=context;
            this.genreConverter = new GenreConverter();
        }
        public Journal JournalDTOToJournal(Models.AbstractItem journal)
        {
            Journal journalToSend = new Journal
            {
                Genre = genreConverter.GenreDTOToGenre(context.Genres.FirstOrDefault(x => x.GenreId == journal.IdofGenre)),
                Discount = (int)journal.Discount,
                ItemID = (int)journal.ItemId,
                Name = journal.Name,
                Price = (int)journal.Price,
                PrintDate = DateTime.Parse(journal.PrintDate),
                Publisher = journal.Publisher,
                Quantity = (int)journal.Quantity,
                Writer = journal.Writer,
                Subject = journal.Subject
            };
            return journalToSend;
        }

        public Models.AbstractItem JournalToJournalDTO(Journal journal)
        {
            return new Models.AbstractItem
            {
                Discount=journal.Discount,
                IdofGenre=journal.Genre.ID,
                ItemId=journal.ItemID,
                Name=journal.Name,
                Price=journal.Price,
                PrintDate=journal.PrintDate.ToString(),
                Quantity=journal.Quantity,
                Subject=journal.Subject,
                Publisher=journal.Publisher,
                Writer=journal.Writer
            };
        }
    }
}
