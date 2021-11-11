using LibraryRenewal.DAL.Exceptions;
using LibraryRenewal.DAL.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Repositories
{
    public class LibraryQueries : ILibraryQueries
    {
        ILibraryDbContext _context;
        public LibraryQueries( ILibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetAllAuthors()
        {
            try
            {
                return _context.AbstractItems.Select(x => x.Writer).ToList();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new QueryException("Cant get all authors");
            }
        }



        public async Task<List<string>> GetAllPublishers()
        {
            try
            {
                return _context.AbstractItems.Select(x => x.Publisher).ToList();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new QueryException("Cant get all authors");
            }
        }
        public async Task<List<string>> GetAllItemNames()
        {
            try
            {
                return _context.AbstractItems.Select(x => x.Name).ToList();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new QueryException("Cant get all authors");
            }
        }

    }
}
