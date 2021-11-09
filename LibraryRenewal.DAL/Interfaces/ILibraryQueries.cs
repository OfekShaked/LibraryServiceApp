using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Interfaces
{
    public interface ILibraryQueries
    {
        Task<List<string>> GetAllPublishers();
        Task<List<string>> GetAllAuthors();
        Task<List<string>> GetAllItemNames();
    }
}
