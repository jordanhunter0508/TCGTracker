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
    }
}
