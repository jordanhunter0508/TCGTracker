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
        private readonly Window _owner = Application.Current.MainWindow;

        private List<Game> _games = new List<Game>();
        private Game _selectedGame;

        private bool _isWindowOpen;                     // Used to "disable" the buttons to prevent multiple windows

        public ICommand AddGameCommand { get; }
        public ICommand EditGameCommand { get; }
        public ICommand ActivateGameCommand { get; }


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
        /// Keeps track of the selected game
        /// </summary>
        public Game SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
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
            AddGameCommand = new RelayCommand(DisplayAddGameWindow, () => !_isWindowOpen);
            EditGameCommand = new RelayCommand(DisplayEditGameWindow, () => !_isWindowOpen);
            ActivateGameCommand = new RelayCommand(ActivateGame);

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
                MessageBox.Show(_owner, ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Loads the Add window for games
        /// </summary>
        private void DisplayAddGameWindow()
        {
            _isWindowOpen = true;
            GameManageWindow window = new GameManageWindow();

            // Set the owner then display the GameManageWindow
            window.Owner = _owner;
            window.ShowDialog();

            // Once closed this runs
            _isWindowOpen = false;

            // If an game was created from the window reload the games
            if (window.DataContext is GameAddViewModel vm && vm.WasSaved)
            {
                LoadGames();
            }
        }

        /// <summary>
        /// Loads the Edit window for games
        /// </summary>
        private void DisplayEditGameWindow()
        {
            if (SelectedGame == null)
            {
                return;
            }

            _isWindowOpen = true;
            
            int gameID = SelectedGame.GameID;
            GameManageWindow window = new GameManageWindow(gameID);

            window.Owner = _owner;
            window.ShowDialog();

            _isWindowOpen = false;

            // If an game was updated from the window reload the games
            if (window.DataContext is GameAddViewModel vm && vm.WasSaved)
            {
                LoadGames();
            }
        }

        /// <summary>
        /// Displays a confirmation window if the user says yes then tries to 
        /// (de/re) activate the game
        /// </summary>
        private void ActivateGame()
        {
            if (SelectedGame == null)
            {
                return;
            }

            string message = SelectedGame.Active ? 
                $"Are you sure you want to deactivate the game {SelectedGame.Name}." : 
                $"Are you sure you want to reactivate the game {SelectedGame.Name}.";

            string title = SelectedGame.Active ?
                $"Confirm {SelectedGame.Name}'s Deactivation" :
                $"Confirm {SelectedGame.Name}'s Reactivation";

            MessageBoxResult confirmationWindow = MessageBox.Show(message,
                                                                  title,
                                                                  MessageBoxButton.YesNo,
                                                                  MessageBoxImage.Warning);

            if (confirmationWindow == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                bool wasUpdated = _gameManager.ActivateGame(SelectedGame.GameID, !SelectedGame.Active);

                if (wasUpdated)
                {
                    MessageBox.Show($"The game {SelectedGame.Name}'s activate status has been successfully updated.");
                    LoadGames();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
