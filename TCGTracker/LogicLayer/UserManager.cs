using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        IUserAccessor _userAccessor;

        /// <summary>
        /// General UserManger created for the presentaion layer
        /// </summary>
        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data or any other IUserAccessor
        /// </summary>
        /// <param name="userAccessor">Set the IUserAccessor in the UserManager</param>
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public string HashSha256(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password was null or blank.");
            }

            string result = string.Empty;

            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2").ToLower());
            }

            result = s.ToString();

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public bool AuthenticateUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email was null or blank.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password was null or blank.");
            }

            bool result = false;

            try
            {
                password = HashSha256(password);
                result = (1 == _userAccessor.AuthenticateUserByEmailAndPasswordHash(email, password));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Authentication failed.\n\n", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email was null or blank.");
            }

            User result = null;

            try
            {
                result = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public UserVM LoginUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email was null or blank.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password was null or blank.");
            }

            UserVM result = null;
            try
            {
                if (AuthenticateUser(email, password))
                {
                    User user = GetUserByEmail(email);
                    result = new UserVM()
                    {
                        UserID = user.UserID,
                        GivenName = user.GivenName,
                        Surname = user.Surname,
                        Email = user.Email,
                        Active = user.Active,
                        Roles = GetRolesForUser(email),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Faild to log in.", ex);
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserManager"/>
        /// </summary>
        public List<string> GetRolesForUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email was null or blank.");
            }

            List<string> results = null;

            try
            {
                results = _userAccessor.SelectRoleByUserEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found.", ex);
            }

            return results;
        }
    }
}
