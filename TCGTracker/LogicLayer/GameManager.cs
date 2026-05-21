using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class GameManager : IGameManager
    {
        IGameAccessor _gameAccessor;

        /// <summary>
        /// General GameManager created for the presentaion layer
        /// </summary>
        public GameManager() 
        {
            _gameAccessor = new GameAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data or any other IGameAccessor
        /// </summary>
        /// <param name="gameAccessor">Set the IGameAccessor in the GameManager</param>
        public GameManager(IGameAccessor gameAccessor)
        { 
            _gameAccessor = gameAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IGameManager"/>
        /// </summary>
        public Game GetGame(int gameID)
        {
            Game result = null;

            try
            {
                result = _gameAccessor.SelectGame(gameID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to find a game with the id {gameID}.",ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IGameManager"/>
        /// </summary>
        public IReadOnlyList<Game> GetAllGames()
        {
            IReadOnlyList<Game> results = new List<Game>();

            try
            {
                results = _gameAccessor.SelectAllGames();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of all games.",ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IGameManager"/>
        /// </summary>
        public int AddGame(Game game)
        {
            if(game == null)
            {
                throw new ArgumentNullException("Cannot save information from a null game.");
            }

            int newID = 4;

            try
            {
                newID = _gameAccessor.InsertGame(game);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add your game to the database.\nPlease verify there isn't a game with the same name.",ex);
            }

            return newID;
        }

        /// <summary>
        /// Implements from <see cref="IGameManager"/>
        /// </summary>
        public bool EditGame(Game game)
        {
            bool wasUpdated = false;

            if (game == null)
            {
                throw new ArgumentNullException("Cannot update a game's information from a null game.");
            }

            try
            {
                wasUpdated = (1 == _gameAccessor.UpdateGame(game));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update the game.\nPlease make sure there isn't a game with the same name.",ex);
            }

            return wasUpdated;
        }

        /// <summary>
        /// Implements from <see cref="IGameManager"/>
        /// </summary>
        public bool ActivateGame(int gameID, bool active)
        {
            bool result = true;

            try
            {
                result = (1 == _gameAccessor.ActivateGame(gameID,active));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update a games active status.",ex);
            }

            return result;
        }
    }
}
