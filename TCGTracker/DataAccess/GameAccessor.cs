using System;
using System.Collections.Generic;
using System.Data;
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
        /// using sp_select_game
        /// </summary>
        public Game SelectGame(int gameID)
        {
            Game result = null;

            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_select_game";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GameID", gameID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = new Game()
                    {
                        GameID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Publisher = reader.GetString(2),
                        OfficialWebsite = reader.IsDBNull(3) ? null : reader.GetString(3),
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
        /// Implements from <see cref="IGameAccessor"/>. Access the database
        /// using sp_select_all_games
        /// </summary>
        public List<Game> SelectAllGames()
        {
            List<Game> results = new List<Game>();
            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_select_all_games";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
                        OfficialWebsite = reader.IsDBNull(3) ? null : reader.GetString(3),
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

        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Access the database
        /// using sp_insert_game
        /// </summary>
        public int InsertGame(Game game)
        {
            int newID = 0;

            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_insert_game";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Publisher", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OfficialWebsite", SqlDbType.NVarChar, 250);

            cmd.Parameters["@Name"].Value = game.Name;
            cmd.Parameters["@Publisher"].Value = game.Publisher;
            cmd.Parameters["@OfficialWebsite"].Value = (object)game.OfficialWebsite ?? DBNull.Value;

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return newID;
        }

        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Access the database
        /// using sp_update_game
        /// </summary>
        public int UpdateGame(Game game)
        {
            int rowCount = 0;

            SqlConnection conn = DbConnection.GetConnection();
            string cmdText = "sp_update_game";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GameID", game.GameID);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Publisher", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OfficialWebsite", SqlDbType.NVarChar, 250);

            cmd.Parameters["@Name"].Value = game.Name;
            cmd.Parameters["@Publisher"].Value = game.Publisher;
            cmd.Parameters["@OfficialWebsite"].Value = (object)game.OfficialWebsite ?? DBNull.Value;

            try
            {
                conn.Open();
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                conn.Close();
            }

            return rowCount;
        }
    }
}