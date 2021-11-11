using LibraryRenewal.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.DAL.Interfaces
{
    public interface ILibraryDbContext
    {
        DbSet<AbstractItem> AbstractItems { get; set; }
        DbSet<Sale> Sales { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Genre> Genres { get; set; }
        Task<int> SaveChangesAsyncInherited();
        void DetachEntity<T>(T entity);
    }
}
