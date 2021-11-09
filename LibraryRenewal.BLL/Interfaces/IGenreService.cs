using LibraryRenewal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Interfaces
{
    public interface IGenereService
    {
        Task<List<string>> GetAllGenreNames();
        Task<Genre> GetGenre(string name);
        Task<bool> CheckGenre(string name);
        Task AddNewGenre(string name);
        Task DeleteGenre(string name);
        Task UpdateGenre(string name, string newName);
    }
}
