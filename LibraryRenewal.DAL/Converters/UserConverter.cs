using LibraryRenewal.Common.Enums;
using LibraryRenewal.DAL.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Converters
{
    public class UserConverter : IUserConverter
    {
        public Common.Models.User UserDTOToUser(Models.User user)
        {
            if (user == null) return null;
            return new Common.Models.User
            {
                FullName = user.FullName,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Type = (UserType)Enum.Parse(typeof(UserType), user.UserType),
                UserID = (int)user.UserId,
                Username = user.Username
            };
        }

        public Models.User UserToUserDTO(Common.Models.User user)
        {
            if (user == null) return null;
            return new Models.User
            {
                FullName = user.FullName,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                UserId = user.UserID,
                Username = user.Username,
                UserType = user.Type.ToString()
            };
        }
    }
}
