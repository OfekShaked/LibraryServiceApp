using LibraryRenewal.Common.Enums;
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
    public class GeneralRepository : IGeneralRepository
    {
        IGenreRepository _genreRep;
        ILibraryDbContext _context;
        IAbstractItemConverter _abstractItemConverter;
        public GeneralRepository(IGenreRepository genreRep, ILibraryDbContext context, IAbstractItemConverter converter)
        {
            _genreRep = genreRep;
            _context = context;
            _abstractItemConverter = converter;
        }

        public static async Task SaveToLogFile(string error)
        {
            //List<string> errors = new List<string>();
            //errors.Add(error + "\n" + DateTime.Now + "\n\n");
            //StorageFolder folder = ApplicationData.Current.LocalFolder;
            //StorageFile file;
            //if (await folder.TryGetItemAsync($"{FileNames.DALLog.ToString()}.txt") == null)
            //{
            //    file = await folder.CreateFileAsync($"{FileNames.DALLog.ToString()}.txt", CreationCollisionOption.ReplaceExisting);
            //}
            //else
            //{
            //    file = await folder.GetFileAsync($"{FileNames.DALLog.ToString()}.txt");
            //}
            //await FileIO.AppendLinesAsync(file, errors);
        }
        public async Task<AbstractItem> GetItemByID(int id)
        {
            try
            {
                return _abstractItemConverter.AbstractItemDTOToAbstractItem(_context.AbstractItems.FirstOrDefault(x => x.ItemId == id));
            }
            catch (Exception e)
            {
                await SaveToLogFile(e.ToString());
                throw new GeneralItemException("Cant get Item By ID");
            }

        }
        public async Task UpdateAbstractItem(int id, AbstractItem updatedItem)
        {
            try
            {
                updatedItem.ItemID = id;
                _context.AbstractItems.Update(_abstractItemConverter.AbstractItemToAbstractItemDTO(updatedItem));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await SaveToLogFile(e.ToString());
                throw new GeneralItemException("Cant get Update Abstract Item");
            }
        }
    }
}
