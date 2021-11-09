using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Interfaces.Converters
{
    public interface IGenreConverter
    {
        Common.Models.Genre GenreDTOToGenre(Models.Genre genre);
        Models.Genre GenreToGenreDTO(Common.Models.Genre genre);
    }
}
