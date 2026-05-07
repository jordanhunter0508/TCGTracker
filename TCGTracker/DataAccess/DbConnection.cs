using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class DbConnection
    {
        /// <summary>
        /// Establish a connection to the database
        /// </summary>
        /// <returns>Returns a connection object to the tcg_tracker_db database</returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = null;

            string connectionString = "Data Source=localhost;Initial Catalog=tcg_tracker_db;Integrated Security=True;Trust Server Certificate=True";
            conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
