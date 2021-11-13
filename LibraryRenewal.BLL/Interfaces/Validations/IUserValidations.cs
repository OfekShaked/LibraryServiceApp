using LibraryRenewal.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Interfaces.Validations
{
    public interface IUserValidations
    {
        bool IsUserValid(string username, string password, string fullName, string phoneNumber, UserType type);
    }
}
