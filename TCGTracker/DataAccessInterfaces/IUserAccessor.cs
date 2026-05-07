using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        /// <summary>
        /// Requests from the database the number of users with the specified email,the specified password hash <br/>
        /// and have an active account.
        /// </summary>
        /// <param name="email">Compared against the emails stored in the database</param>
        /// <param name="passwordHash">Compared against the passwordHash stored in the database</param>
        /// <returns>Returns the count of users with the corresponding email and passwordHash</returns>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash);

        /// <summary>
        /// Request from the database all fields execpt the passwordHash.<br/>
        /// Where the the email parameter matches one stored in the database.
        /// </summary>
        /// <param name="email">Compared against the emails stored in the database</param>
        /// <returns>Returns a User object that was found with the matching email</returns>
        public User SelectUserByEmail(string email);

        /// <summary>
        /// Request from the database the RoleIDs. 
        /// Where the email parameter matches one stored in the database.
        /// </summary>
        /// <param name="email">Compared against the emails stored in the database</param>
        /// <returns>Returns a list of strings that are roles of a specific user</returns>
        public List<string> SelectRoleByUserEmail(string email);
    }
}
