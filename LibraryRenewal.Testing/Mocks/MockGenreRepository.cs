using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Testing.Mocks
{
    class MockGenreRepository : IGenreRepository
    {
        List<Genre> genres = new List<Genre>();
        public async Task AddGenre(Genre g1)
        {
            genres.Add(g1);
        }

        public async Task DeleteGenre(Genre g1)
        {
            for (int i = 0; i < genres.Count; i++)
            {
                if (genres[i].Name.Equals(g1.Name))
                {
                    genres.RemoveAt(i);
                    return;
                }
            }
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return genres;
        }

        public async Task<Genre> GetGenre(string name)
        {
            for (int i = 0; i < genres.Count; i++)
            {
                if (genres[i].Name.Equals(name))
                {
                    return genres[i];
                }
            }
            return new Genre(name);
        }

        public async Task<Genre> GetGenreByID(int id)
        {
            for (int i = 0; i < genres.Count; i++)
            {
                if (genres[i].ID.Equals(id))
                { return genres[i]; }
            }
            return null;
        }

        public async Task UpdateGenre(int genreID, Genre updatedGenre)
        {
            for (int i = 0; i < genres.Count; i++)
            {
                if (genres[i].ID.Equals(genreID))
                {
                    updatedGenre.ID = genreID;
                    genres[i] = updatedGenre;
                }
            }
        }
    }
}
