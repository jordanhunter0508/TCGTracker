using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class UserAccessor : IUserAccessor
    {
        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_authenticate_user
        /// </summary>
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            int count = 0;

            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_authenticate_user";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_select_user_by_email
        /// </summary>
        public User SelectUserByEmail(string email)
        {
            User result = null;

            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_select_user_by_email";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = new User()
                    {
                        UserID = reader.GetInt32(0),
                        GivenName = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Email = reader.GetString(3),
                        Active = reader.GetBoolean(4),
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IUserAccessor"/>. Access the database
        /// using sp_select_role_by_email
        /// </summary>
        public IReadOnlyList<string> SelectRoleByUserEmail(string email)
        {
            List<string> results = new List<string>();

            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_select_roles_by_email";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return results;
        }        
    }
}
