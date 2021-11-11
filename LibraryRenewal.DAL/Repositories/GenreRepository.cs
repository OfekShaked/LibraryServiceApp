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
    public class GenreRepository : IGenreRepository
    {
        ILibraryDbContext _context;
        IGenreConverter _converter;
        public GenreRepository(ILibraryDbContext context, IGenreConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task AddGenre(Genre genre)
        {
            try
            {
                _context.Genres.Add(_converter.GenreToGenreDTO(genre));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new GenreException("Cant add genre");
            }
        }

        public async Task DeleteGenre(Genre genre)
        {
            try
            {
                _context.Genres.Remove(_context.Genres.FirstOrDefault(x => x.GenreId == genre.ID));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new GenreException("Cant delete genre");
            }
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                var genresFound = _context.Genres.ToList();
                genresFound.ForEach(x => genres.Add(_converter.GenreDTOToGenre(x)));
                return genres;
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new GenreException("Cant get all genres");
            }
        }

        public async Task<Genre> GetGenre(string name)
        {
            try
            {
                return _converter.GenreDTOToGenre(_context.Genres.FirstOrDefault(x => x.GenreName == name));
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new GenreException("Cant get genre by name");
            }
        }
        public async Task<Genre> GetGenreByID(int id)
        {
            try
            {
                return _converter.GenreDTOToGenre(_context.Genres.FirstOrDefault(x => x.GenreId == id));
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new GenreException("Cant get genre by id");
            }
        }

        public async Task UpdateGenre(int genreID, Genre updatedGenre)
        {
            try
            {
                updatedGenre.ID = genreID;
                var genre = _context.Genres.FirstOrDefault(x => x.GenreId == genreID);
                genre.GenreName = updatedGenre.Name;
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new GenreException("Cant update genre");
            }
        }
    }
}
