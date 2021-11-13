using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Testing.Mocks
{
    class MockUserRepository : IUserRepository
    {
        List<User> users = new List<User>();
        public async Task AddUser(User u1)
        {
            users.Add(u1);
        }

        public async Task DeleteUser(User u1)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserID.Equals(u1.UserID))
                {
                    users.RemoveAt(i);
                    return;
                }
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return users;
        }

        public async Task<User> GetUser(string username)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username.Equals(username))
                {
                    return users[i];
                }
            }
            return null;
        }

        public async Task UpdateUser(string username, User updatedUser)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username.Equals(username))
                {
                    updatedUser.UserID = users[i].UserID;
                    users[i] = updatedUser;
                }
            }
        }
    }
}
