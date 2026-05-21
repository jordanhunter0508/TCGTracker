using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class UserAccessorFakes : IUserAccessor
    {
        private List<User> _users;
        private List<UserVM> _userVMs;

        private string _passwordHash;

        /// <summary>
        /// Fills the _users list with fake data
        /// </summary>
        public UserAccessorFakes()
        {
            _users = new List<User>();
            _users.Add(new User()
            {
                UserID = 1,
                GivenName = "test",
                Surname = "user",
                Email = "testuser1@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 2,
                GivenName = "john",
                Surname = "doe",
                Email = "testuser2@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 3,
                GivenName = "frank",
                Surname = "smith",
                Email = "testuser3@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 4,
                GivenName = "thomas",
                Surname = "tank",
                Email = "testuser4@test.com",
                Active = true,
            });
            _users.Add(new User()
            {
                UserID = 5,
                GivenName = "leonardo",
                Surname = "turtle",
                Email = "testuser5@test.com",
                Active = false,
            });

            _userVMs = new List<UserVM>();
            _userVMs.Add(new UserVM()
            {
                UserID = _users[0].UserID,
                GivenName = _users[0].GivenName,
                Surname = _users[0].Surname,
                Email = _users[0].Email,
                Active = _users[0].Active,
                Roles = new List<String>() { "testRole1", "testRole2" },
            });
            _userVMs.Add(new UserVM()
            {
                UserID = _users[1].UserID,
                GivenName = _users[1].GivenName,
                Surname = _users[1].Surname,
                Email = _users[1].Email,
                Active = _users[1].Active,
                Roles = new List<String>() { "testRole3", "testRole4" },
            });
            _userVMs.Add(new UserVM()
            {
                UserID = _users[2].UserID,
                GivenName = _users[2].GivenName,
                Surname = _users[2].Surname,
                Email = _users[2].Email,
                Active = _users[2].Active,
                Roles = new List<String>() { },
            });

            _passwordHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Used for tests
        /// </summary>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;
            count = _users.Where(u => u.Email.Equals(email) && 
                                      _passwordHash.Equals(passwordHash) &&
                                      u.Active).Count();
            return count;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Used for tests
        /// </summary>
        public User SelectUserByEmail(string email)
        {
            User result = null;
            result = _users.FirstOrDefault(u => u.Email.Equals(email));
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Used for tests
        /// </summary>
        public IReadOnlyList<string> SelectRoleByUserEmail(string email)
        {
            List<string> roles = new List<string>();

            UserVM user = _userVMs.FirstOrDefault(u => u.Email.Equals(email));

            if (user != null)
            {
                roles = user.Roles;
            }

            return roles;
        }
    }
}
