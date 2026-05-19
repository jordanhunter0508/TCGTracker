using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace Desktop.ViewModels
{
    public class GameListViewModel
    {
        private IGameManager _gameManager;

        private List<Game> _games;

        /// <summary>
        /// List of all games from the database
        /// </summary>
        public List<Game> Games 
        {
            get => _games;
        }

        /// <summary>
        /// Initalize the Manager and the list
        /// </summary>
        public GameListViewModel()
        {
            _gameManager = new GameManager();

            _games = new List<Game>();
           LoadGames();
        }

        /// <summary>
        /// Gets the List of all games and saves it to the 
        /// private list of games
        /// </summary>
        private void LoadGames()
        {
            try
            {
                _games = _gameManager.GetAllGames();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }
    }
}
