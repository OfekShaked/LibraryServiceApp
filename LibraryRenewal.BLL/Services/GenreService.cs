using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces;
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
    class GenreService : IGenereService
    {
        IGenreRepository _genreRep;
        IGeneralRepository _generalRep;
        public GenreService(IGenreRepository genreRep, IGeneralRepository generalRep)
        {
            _generalRep = generalRep;
            _genreRep = genreRep;
        }
        public async Task AddNewGenre(string name)
        {
            try
            {
                if (await CheckGenre(name) == false)
                {

                    Genre g1 = new Genre(name);
                    await _genreRep.AddGenre(g1);
                }
                else
                {
                    await GeneralLibraryLogic.SaveToLogFile("Genre Already Exist");
                    throw new LibraryException("Genre already exist");
                }
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot add a new genre atm try again later or call a manager");
            }
        }

        private static async Task CatchException(Exception e, string message)
        {
            if (e is GenreException
                                 || e is DALException)
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new BLLGenreException(message);
            }
            else
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new LibraryException("Unknown error inform a manager!");
            }
        }

        public async Task<bool> CheckGenre(string name)
        {
            try
            {
                Genre gen = await _genreRep.GetGenre(name);
                if (gen.Name == null)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot check a genre atm try again later or call a manager");
                return false;
            }

        }

        public async Task DeleteGenre(string name)
        {
            try
            {
                Genre g1 = await GetGenre(name);
                if (g1.Name != null)
                    await _genreRep.DeleteGenre(g1);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot delete a genre atm try again later or call a manager");
            }
        }

        public async Task<List<string>> GetAllGenreNames()
        {
            List<string> genres = new List<string>();
            try
            {
                List<Genre> genresL = await _genreRep.GetAllGenres();
                for (int i = 0; i < genresL.Count; i++)
                {
                    genres.Add(genresL[i].Name);
                }
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot get all genres atm try again later or call a manager");
            }
            return genres;
        }
        /// <summary>
        /// Get a genre by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Genre found or an empty genre if not found</returns>
        public async Task<Genre> GetGenre(string name)
        {
            try
            {
                return await _genreRep.GetGenre(name);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot get a genre atm try again later or call a manager");
                return null;
            }
        }

        public async Task UpdateGenre(string name, string newName)
        {
            try
            {
                Genre g1 = await GetGenre(name);
                Genre updated = new Genre(name);
                updated.ID = g1.ID;
                updated.Name = newName;
                await _genreRep.UpdateGenre(updated.ID, updated);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot update a genre atm try again later or call a manager");
            }
        }
    }
}
