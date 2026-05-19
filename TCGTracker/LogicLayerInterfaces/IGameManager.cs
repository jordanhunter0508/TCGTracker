using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IGameManager
    {
        /// <summary>
        /// Gets a list of all games from the database
        /// </summary>
        /// <returns>Returns a list of all games.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database</exception>
        public List<Game> GetAllGames();

        /// <summary>
        /// Add a new Game to the database.
        /// </summary>
        /// <param name="game">Game to add to the database</param>
        /// <returns>Returns the new ID of the game</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database</exception>
        public int AddGame(Game game);
    }
}
