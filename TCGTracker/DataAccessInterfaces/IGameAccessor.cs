using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IGameAccessor
    {
        /// <summary>
        /// Requests a game from the database
        /// </summary>
        /// <param name="gameID">Id of the game being requested</param>
        /// <returns>Returns a game where the gameID matches one in the database</returns>
        public Game SelectGame(int gameID);

        /// <summary>
        /// Requests a list of all games from the database.
        /// </summary>
        /// <returns>Returns a list of all games from the database.</returns>
        public List<Game> SelectAllGames();

        /// <summary>
        /// Inserts a new game into the database then returns the new Id of the game.
        /// </summary>
        /// <param name="game">New Game to insert</param>
        /// <returns>Returns the new ID of the inserted game</returns>
        public int InsertGame(Game game);

        /// <summary>
        /// Updates the database to match the game where the GameIDs match
        /// </summary>
        /// <param name="game">Updated version of the game</param>
        /// <returns>Returns number of rows affected</returns>
        public int UpdateGame(Game game);

        /// <summary>
        /// Updates the Active field for the game with a 
        /// matching gameID to the active parameter.
        /// </summary>
        /// <param name="gameID">Used to find the game that will be updated.</param>
        /// <param name="active">New active status of the game.</param>
        /// <returns>Returns number of rows affected</returns>
        public int ActivateGame(int gameID, bool active);
    }
}
