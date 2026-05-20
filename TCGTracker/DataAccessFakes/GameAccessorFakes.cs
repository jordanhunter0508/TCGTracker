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
        public Game SelectGame(int gameID)
        {
            Game result = new Game();
            result = _games.FirstOrDefault(g => g.GameID == gameID);
            return result;
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

        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Used for tests
        /// </summary>
        public int InsertGame(Game game)
        {
            int newID = 0;

            // Check to make sure invalid nulls don't make it through
            if (string.IsNullOrWhiteSpace(game.Name))
            {
                throw new ArgumentException("The game can not have a null name.");
            }
            if (string.IsNullOrWhiteSpace(game.Publisher))
            {
                throw new ArgumentException("The game can not have a null publisher.");
            }

            // Check to make sure a unqiue field is not invalidated
            bool containsName = _games.Where(g => g.Name.Equals(game.Name, StringComparison.OrdinalIgnoreCase)).Any();
            if (containsName)
            {
                throw new Exception("A game with this name is already stored.");
            }

            _games.Add(game);
            newID = _games.Count;

            return newID;
        }

        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Used for tests
        /// </summary>
        public int UpdateGame(Game game)
        {
            int rowCount = 0;

            // Check to make sure invalid nulls don't make it through
            if (string.IsNullOrWhiteSpace(game.Name))
            {
                throw new ArgumentException("The game can not have a null name.");
            }
            if (string.IsNullOrWhiteSpace(game.Publisher))
            {
                throw new ArgumentException("The game can not have a null publisher.");
            }

            // Check to make sure a unqiue field is not invalidated
            bool containsName = _games.Where(g => g.Name.Equals(game.Name, StringComparison.OrdinalIgnoreCase)).Any();
            if (containsName)
            {
                throw new Exception("A game with this name is already stored.");
            }

            foreach (Game oldGame in _games)
            {
                if (oldGame.GameID == game.GameID)
                {
                    oldGame.Name = game.Name;
                    oldGame.Publisher = game.Publisher;
                    oldGame.OfficialWebsite = game.OfficialWebsite;
                    rowCount++;
                }
            }

            return rowCount;
        }

        /// <summary>
        /// Implements from <see cref="IGameAccessor"/>. Used for tests
        /// </summary>
        public int ActivateGame(int gameID, bool active)
        {
            int rowCount = 0;

            foreach (Game game in _games)
            {
                if (game.GameID == gameID)
                {
                    game.Active = active;
                    rowCount++;
                }
            }

            return rowCount;
        }
    }
}
