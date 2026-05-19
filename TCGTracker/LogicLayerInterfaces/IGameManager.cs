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
    }
}
