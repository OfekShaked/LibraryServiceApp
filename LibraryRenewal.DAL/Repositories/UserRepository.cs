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
    public class UserRepository : IUserRepository
    {
        ILibraryDbContext _context;
        IUserConverter _converter;

        public UserRepository(ILibraryDbContext context, IUserConverter converter)
        {
            _context = context;
            _converter = converter;
        }
        public async Task AddUser(User user)
        {
            try
            {
                _context.Users.Add(_converter.UserToUserDTO(user));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new UserException("Cant add user");
            }
        }

        public async Task DeleteUser(User user)
        {
            try
            {
                _context.Users.Remove(_converter.UserToUserDTO(user));
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new UserException("Cant delete user");
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                List<User> users = new List<User>();
                _context.Users.ToList().ForEach(x => users.Add(_converter.UserDTOToUser(x)));
                return users;
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new UserException("Cant get all users");
            }
        }

        public async Task<User> GetUser(string username)
        {
            try
            {
                return _converter.UserDTOToUser(_context.Users.FirstOrDefault(x => x.Username == username));
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new UserException("Cant get user");
            }
        }

        public async Task UpdateUser(string username, User updatedUser)
        {
            try
            {
                var updated = _context.Users.FirstOrDefault(x => x.Username == username);
                updated.FullName = updatedUser.FullName;
                updated.PhoneNumber = updatedUser.PhoneNumber;
                updated.UserType = updatedUser.Type.ToString();
                await _context.SaveChangesAsyncInherited();
            }
            catch (Exception e)
            {
                await GeneralRepository.SaveToLogFile(e.ToString());
                throw new UserException("Cant update user");
            }
        }
    }
}
