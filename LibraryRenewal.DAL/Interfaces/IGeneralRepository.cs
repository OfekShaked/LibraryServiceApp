using LibraryRenewal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Interfaces
{
    public interface IGeneralRepository
    {
        Task<AbstractItem> GetItemByID(int id);
        Task UpdateAbstractItem(int id, AbstractItem updatedItem);
    }
}
