using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces;
using LibraryRenewal.BLL.Interfaces.Validations;
using LibraryRenewal.Common.Enums;
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
    public class UserValidity : IUserValidity
    {
        IUserRepository _userRep;
        IGeneralRepository _generalRep;
        IUserValidations _userValidations;
        public UserValidity(IUserRepository userRep, IGeneralRepository generalRep, IUserValidations userValidations)
        {
            _userRep = userRep;
            _generalRep = generalRep;
            _userValidations = userValidations;
        }
        public async Task AddNewUser(string username, string password, string fullName, string phoneNumber, UserType type)
        {
            try
            {
                var isValid = _userValidations.IsUserValid(username, password, fullName, phoneNumber, type);
                User u1 = new User();
                if (await CheckForUser(username) == UserType.Manager || await CheckForUser(username) == UserType.Employee)
                {
                    await GeneralLibraryLogic.SaveToLogFile("Username already exist");
                    throw new BLLUserException("Username already exist");
                }
                u1.Username = username;
                u1.Password = password;
                u1.FullName = fullName;
                u1.PhoneNumber = phoneNumber;
                u1.Type = type;
                await _userRep.AddUser(u1);
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot add a new user atm try again later or call a manager");
            }
        }
        /// <summary>
        /// Checks if the username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserType of the user or Null UserType</returns>
        private async Task<UserType> CheckForUser(string username)
        {
            try
            {
                User res = await _userRep.GetUser(username);
                if (res != null)
                    return res.Type;
                return UserType.Null;
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot check for a user atm try again later or call a manager");
                return UserType.Null;
            }
        }
        /// <summary>
        /// Checks if a user exists and checks if the password enters matches the username's password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserType> VerifyUser(string username, string password)
        {
            try
            {
                if (await CheckForUser(username) == UserType.Null)
                    return UserType.Null;
                User u1 = await _userRep.GetUser(username);
                if (u1.CheckPasswordValidity(password) == true)
                    return u1.Type;
                return UserType.Null;
            }
            catch (Exception e)
            {
                await CatchException(e, "Cannot verify a user atm try again later or call a manager");
                return UserType.Null;
            }
        }

        private static async Task CatchException(Exception e,string message)
        {
            if (e is UserException
                                 || e is DALException)
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new BLLUserException(message);
            }
            else
            {
                await GeneralLibraryLogic.SaveToLogFile(e.ToString());
                throw new LibraryException("Unknown error inform a manager!");
            }
        }
    }
}
