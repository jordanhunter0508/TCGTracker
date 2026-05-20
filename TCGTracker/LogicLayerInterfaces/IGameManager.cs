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
        /// Gets a game with a matching gameID.
        /// </summary>
        /// <param name="gameID">Id of the game being requested.</param>
        /// <returns>Returns a fully qualified game if possible.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database.</exception>
        public Game GetGame(int gameID);

        /// <summary>
        /// Gets a list of all games from the database.
        /// </summary>
        /// <returns>Returns a list of all games.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database.</exception>
        public List<Game> GetAllGames();

        /// <summary>
        /// Add a new Game to the database.
        /// </summary>
        /// <param name="game">Game to add to the database.</param>
        /// <returns>Returns the new ID of the game.</returns>
        /// <exception cref="ApplicationException">
        /// Throws if there is an error connecting to the database or a foreign key violation.
        /// </exception>
        public int AddGame(Game game);

        /// <summary>
        /// Updates the game with the same gameID to the passed in game.
        /// </summary>
        /// <param name="game">Updated version of the game.</param>
        /// <returns>Returns true if only one row was affected.</returns>
        /// <exception cref="ApplicationException">
        /// Throws if there is an error connecting to the database or a foreign key violation.
        /// </exception>
        public bool EditGame(Game game);

        /// <summary>
        /// Updates the active status a game with a matching a gameID.
        /// </summary>
        /// <param name="gameID">Used to find the game that will be updated.</param>
        /// <param name="active">New active status of the game.</param>
        /// <returns>Returns true if only one row was affected false otherwise.</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database.</exception>
        public bool ActivateGame(int gameID, bool active);
    }
}
