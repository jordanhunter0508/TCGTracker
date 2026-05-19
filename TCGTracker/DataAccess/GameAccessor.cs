using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class GameAccessor : IGameAccessor
    {
        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Access the database
        /// using sp_select_all_games
        /// </summary>
        public List<Game> SelectAllGames()
        {
            List<Game> results = new List<Game>();
            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_select_all_games";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new Game()
                    { 
                        GameID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Publisher = reader.GetString(2),
                        OfficialWebsite = reader.GetString(3),
                        Active = reader.GetBoolean(4),
                    });
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
