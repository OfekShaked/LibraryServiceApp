using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces.Validations;
using LibraryRenewal.BLL.Services;
using LibraryRenewal.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Validations
{
    public class UserValidations : IUserValidations
    {
        public bool IsUserValid(string username, string password, string fullName, string phoneNumber, UserType type)
        {
            if (username == "" || password == "" || fullName == "" || phoneNumber == "" || type == UserType.Null)
            {
                Task.Run(()=> GeneralLibraryLogic.SaveToLogFile("All user fields must be full!"));
                throw new BLLUserException("All user fields must be full!");
            }
            return true;
        }
    }
}
