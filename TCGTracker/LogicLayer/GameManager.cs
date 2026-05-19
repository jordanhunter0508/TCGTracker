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
        public List<Game> GetAllGames()
        {
            List<Game> results = new List<Game>();

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
    }
}
