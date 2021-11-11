using LibraryRenewal.DAL.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Converters
{
    public class GenreConverter : IGenreConverter
    {
        public Common.Models.Genre GenreDTOToGenre(Models.Genre genre)
        {
            if (genre == null) return null;
            return new Common.Models.Genre(genre.GenreName) { ID = (int)genre.GenreId };
        }

        public Models.Genre GenreToGenreDTO(Common.Models.Genre genre)
        {
            if (genre == null) return null;
            return new Models.Genre { GenreId = genre.ID, GenreName = genre.Name };
        }
    }
}
