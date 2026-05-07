using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        /// <summary>
        /// Log the user in and returns a fully qualified UserVM
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <param name="password">Converted to a Hash then used to find a match in the database</param>
        /// <returns>Returns a UserVM object created from the user's information from the database.</returns>
        /// <exception cref="ApplicationException">Throws if the Authentication fails or if either of the Get methods fail</exception>
        /// <exception cref="ArgumentException">Throws if the inputed email or password is null or whitespace</exception>
        public UserVM LoginUser(string email, string password);

        /// <summary>
        /// Verify the user is in the database.
        /// </summary>
        /// <param name="email">String to be used in AuthenticateUserByEmailAndPasswordHash</param>
        /// <param name="password">String to be hashed then used in AuthenticateUserByEmailAndPasswordHash</param>
        /// <returns>Returns true if the user is active and has input a valid email and password. Returns false otherwise</returns>
        /// <exception cref="ApplicationException">Throws if there is an error with the AuthenticateUserByEmailAndPasswordHash</exception>
        /// <exception cref="ArgumentException">Throws if the inputed email or password is null or whitespace</exception>
        public bool AuthenticateUser(string email, string password);

        /// <summary>
        /// Creates a user object from the user's emial
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <returns>A User object from the database that has the matching email</returns>
        /// <exception cref="ApplicationException">Throws if the email is not found in the database</exception>
        /// <exception cref="ArgumentException">Throws if the inputed email is null or whitespace</exception>
        public User GetUserByEmail(string email);

        /// <summary>
        /// Passes parameters to <see href="SelectRoleByUserEmail(string)"/> then returns <br/>
        /// a list of strings of the user roles.
        /// </summary>
        /// <param name="email">Used to search the database for a matching email</param>
        /// <returns>Returns a list of strings that are roles of a specific user</returns>
        /// <exception cref="ApplicationException">Throws if the email is not found in the database</exception>
        /// <exception cref="ArgumentException">Throws if the inputed email is null or whitespace</exception>
        public List<string> GetRolesForUser(string email);

        /// <summary>
        /// Converts the inputed password to a Sha256 string
        /// using SHA256 and a StringBuilder.
        /// </summary>
        /// <param name="password">String turned into the Sha256</param>
        /// <returns>Returns a string of password as a Sha256</returns>
        /// <exception cref="ArgumentException">Throws if the inputed string is null or whitespace</exception>
        public string HashSha256(string password);
    }
}
