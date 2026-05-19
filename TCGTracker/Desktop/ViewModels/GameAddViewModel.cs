using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataDomain;
using Desktop.Views.Pages;
using LogicLayer;
using LogicLayerInterfaces;

namespace Desktop.ViewModels
{
    public class GameAddViewModel : ViewModelBase
    {
        private readonly IGameManager _gameManager;

        private string _name;
        private string _publisher;
        private string _officialWebsite;
        private string _windowTitle;
        private string _errorMessage;

        public ICommand SaveGameCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public Action CloseWindowAction { get; set; }

        /// <summary>
        /// Used to change the name of the window for add or edit mode
        /// </summary>
        public string WindowTitle
        {
            get => _windowTitle;
        }

        /// <summary>
        /// Name of the gamed being created or updated
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                (SaveGameCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Name of the publisher of the game being created or updated
        /// </summary>
        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChanged();
                (SaveGameCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Url of the game's home page
        /// </summary>
        public string OfficialWebsite
        {
            get => _officialWebsite;
            set
            {
                _officialWebsite = value;
                OnPropertyChanged();
                (SaveGameCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Used to display helpful notes to the user
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Used to help load the game list when this window is closed
        /// </summary>
        public bool WasSaved { get; private set; } = false;

        /// <summary>
        /// Initalize the manager and the commands.
        /// Also sets the window title
        /// </summary>
        public GameAddViewModel()
        {
            _gameManager = new GameManager();
            _windowTitle = "Add a New Game";

            SaveGameCommand = new RelayCommand(CreateGame, CanSave);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        /// <summary>
        /// Attempts to save the new game into the database
        /// </summary>
        public void CreateGame()
        {
            if (!IsValid())
            {
                return;
            }

            try
            {
                Game game = new Game()
                {
                    Name = _name.Trim(),
                    Publisher = _publisher.Trim(),
                    OfficialWebsite = _officialWebsite?.Trim(),
                };

                _gameManager.AddGame(game);
                WasSaved = true;

                MessageBox.Show($"The game {game.Name} was successfully  added.");

                CloseWindowAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Validates the user input
        /// </summary>
        /// <returns>Returns false if any of the input fields aren't valid, true otherwise</returns>
        public bool IsValid()
        {
            bool isValid = true;

            if (_name.Trim().Length > 100)
            {
                ErrorMessage = "Name must be less than 100 characters.";
                isValid = false;
            }
            else if (_publisher.Trim().Length > 100)
            {
                ErrorMessage = "Publisher must be less than 100 characters.";
                isValid = false;
            }
            else if (!string.IsNullOrEmpty(_officialWebsite))
            {
                string regexPattern = @"^(https?:\/\/)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/.*)?$";
                if (!Regex.IsMatch(_officialWebsite,regexPattern))
                {
                    ErrorMessage = "Official website was formatted incorrectly.";
                    isValid = false;
                }
                else if (_officialWebsite.Trim().Length > 250)
                {
                    ErrorMessage = "Official website must be less than 250 characters.";
                    isValid = false;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Makes sure the input fields are fillout.
        /// </summary>
        /// <returns>Returns true by default, if any input fields are empty return false.</returns>
        public bool CanSave()
        {
            bool canSave = true;

            if (string.IsNullOrWhiteSpace(_name))
            {
                canSave = false;
            }
            else if (string.IsNullOrWhiteSpace(_publisher))
            {
                canSave = false;
            }

            return canSave;
        }

        /// <summary>
        /// Closes the current window
        /// </summary>
        public void CloseWindow()
        {
            if (CloseWindowAction != null)
            {
                CloseWindowAction();
            }
        }
    }
}
