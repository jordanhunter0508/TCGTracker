using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataDomain;
using Desktop.Views;
using Desktop.Views.Windows;
using LogicLayer;
using LogicLayerInterfaces;

namespace Desktop.ViewModels
{
    public class GameListViewModel : ViewModelBase
    {
        private readonly IGameManager _gameManager;
        private List<Game> _games = new List<Game>();

        private bool _isWindowOpen;                     // Used to "disable" the buttons to prevent multiple windows

        public ICommand AddGameCommand { get; }

        /// <summary>
        /// List of all games from the database
        /// </summary>
        public List<Game> Games
        {
            get => _games;
            set
            {
                _games = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initalize the Manager and the list
        /// </summary>
        public GameListViewModel()
        {
            _gameManager = new GameManager();

            // Commands
            AddGameCommand = new RelayCommand(DisplayManageGameWindow, () => !_isWindowOpen);

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
                Games = _gameManager.GetAllGames();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Loads the Add/Edit window for games
        /// </summary>
        private void DisplayManageGameWindow()
        {
            _isWindowOpen = true;
            GameManageWindow window = new GameManageWindow();

            window.Owner = Application.Current.MainWindow;
            window.Closed += (sender, e) =>
            {
                _isWindowOpen = false;

                // If an game was created from the window reload the games
                if (window.DataContext is GameAddViewModel vm && vm.WasSaved)
                {
                    LoadGames();
                }
            };
            window.Show();
        }
    }
}
