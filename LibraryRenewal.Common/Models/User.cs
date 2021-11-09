using LibraryRenewal.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Common.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public UserType Type { get; set; }
        public User(string username, string password, string fullName, string phone, UserType type)
        {
            this.Username = username;
            this.Password = password;
            this.FullName = fullName;
            this.PhoneNumber = phone;
            this.Type = type;
        }
        public User()
        {

        }
        public bool CheckPasswordValidity(string password)
        {
            return this.Password.Equals(password);
        }
        public override string ToString()
        {
            return $"{UserID},{Username},{Password},{FullName},{PhoneNumber},{Type}";
        }

    }
}
