using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Models
{
    public partial class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
    }
}
