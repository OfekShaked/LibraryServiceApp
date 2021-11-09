using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Interfaces.Converters
{
    public interface IUserConverter
    {
        Common.Models.User UserDTOToUser(Models.User user);
        Models.User UserToUserDTO(Common.Models.User user);
    }
}
