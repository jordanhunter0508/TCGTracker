using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class GameAccessorFakes : IGameAccessor
    {
        List<Game> _games;

        /// <summary>
        /// Fills the _games list with fake data
        /// </summary>
        public GameAccessorFakes()
        {
            _games = new List<Game>();
            _games.Add(new Game()
            { 
                GameID = 1,
                Name = "Game Name 1",
                Publisher = "Publisher 1",
                OfficialWebsite = "https://wwww.website1.com",
                Active = true,
            });
            _games.Add(new Game()
            { 
                GameID = 2,
                Name = "Game Name 2",
                Publisher = "Publisher 1",
                OfficialWebsite = "https://wwww.website2.com",
                Active = true,
            });
            _games.Add(new Game()
            { 
                GameID = 3,
                Name = "Game Name 3",
                Publisher = "Publisher 2",
                OfficialWebsite = null,
                Active = false,
            });
        }

        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Used for tests
        /// </summary>
        public List<Game> SelectAllGames()
        {
            List<Game> results = new List<Game>();
            results = _games;
            return results;
        }
    }
}
