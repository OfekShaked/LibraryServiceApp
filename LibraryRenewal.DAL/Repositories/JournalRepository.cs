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
    public class JournalRepository : IJournalRepository
    {
        ILibraryDbContext _context;
        IJournalConverter _converter;
        public JournalRepository( ILibraryDbContext context, IJournalConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task AddJournal(Journal journal)
        {
            try
            {
                _context.AbstractItems.Add(_converter.JournalToJournalDTO(journal));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new JournalException("Cant add journal");
            }
        }

        public async Task DeleteJournal(Journal journal)
        {
            try
            {
                _context.AbstractItems.Remove(_converter.JournalToJournalDTO(journal));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new JournalException("Cant delete journal");
            }
        }

        public async Task<List<Journal>> GetAllJournals()
        {
            List<Journal> journals = new List<Journal>();
            try
            {
                var journalsFound = _context.AbstractItems.Where(x => x.Isbn == null).ToList();
                journalsFound.ForEach(x => journals.Add(_converter.JournalDTOToJournal(x)));
                return journals;
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new JournalException("Cant get all journals");
            }
        }

        public async Task<Journal> GetJournal(int id)
        {
            try
            {
                var journalFound = _context.AbstractItems.FirstOrDefault(x => x.ItemId == id && x.Isbn == null);
                return _converter.JournalDTOToJournal(journalFound);
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new JournalException("Cant get journal");
            }
        }

        public async Task UpdateJournal(int id, Journal updatedJournal)
        {
            try
            {
                updatedJournal.ItemID = id;
                var journal = _converter.JournalToJournalDTO(updatedJournal);
                var journalFromRep = _context.AbstractItems.FirstOrDefault(x => x.ItemId == id);
                journalFromRep.Quantity = journal.Quantity;
                journalFromRep.Discount = journal.Discount;
                journalFromRep.Price = journal.Price;
                journalFromRep.Edition = journal.Edition;
                journalFromRep.IdofGenre = journal.IdofGenre;
                journalFromRep.Isbn = journal.Isbn;
                journalFromRep.Name = journal.Name;
                journalFromRep.PrintDate = journal.PrintDate;
                journalFromRep.Publisher = journal.Publisher;
                journalFromRep.Summary = journal.Summary;
                journalFromRep.Subject = journal.Subject;
                journalFromRep.Writer = journal.Writer;
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new JournalException("Cant update journal");
            }
        }
    }
}
