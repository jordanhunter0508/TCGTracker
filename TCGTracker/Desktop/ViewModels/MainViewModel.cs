using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataDomain;
using Desktop.Views.Pages;
using LogicLayerInterfaces;

namespace Desktop.ViewModels
{
    /// <summary>
    /// This is the MainWindow's view model
    /// Has each button command and a statusBar text
    /// </summary>
    public class MainViewModel : ViewModelBase
    {


        private UserVM _accessToken;
        private string _statusMessage;
        private bool _isSignedIn;

        // Requests
        public event Action<Page> NavigateRequested;

        // Commands
        public ICommand LogOutButtonCommand { get; }
        public ICommand AttributePageCommand { get; }
        public ICommand GamePageCommand { get; }

        /// <summary>
        /// Updates the status bar on the bottom of the window
        /// </summary>
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Holds the UserVM of the signed in user
        /// Updates the status message when signed in
        /// </summary>
        public UserVM AccessToken
        {
            get { return _accessToken; }
            set
            {
                _accessToken = value;
                OnPropertyChanged();

                if (_accessToken != null)
                {
                    StatusMessage = $"Logged in as {_accessToken.Email}";
                    IsSignedIn = true;
                }
                else
                {
                    StatusMessage = "Please log in to access the desktop application.";
                    IsSignedIn = false;
                }
            }
        }

        /// <summary>
        /// Used to help udate the Log out buttons Visibility
        /// </summary>
        public bool IsSignedIn
        {
            get => _isSignedIn;
            set
            {
                if (_isSignedIn != value)
                {
                    _isSignedIn = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(LogOutButtonVisibility));  // Notify the UI it needs to update the visibility
                }
            }
        }

        /// <summary>
        /// Gets the current visibility of the sign out button
        /// Based on if IsSignedIn is true or false
        /// </summary>
        public Visibility LogOutButtonVisibility
        {
            get
            {
                return IsSignedIn ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Initalize AccessToken to null by default to update the StatusBar
        /// </summary>
        public MainViewModel()
        {
            AccessToken = null;
            LogOutButtonCommand = new RelayCommand(LogOut);

            // Nav bar commands
            AttributePageCommand = new RelayCommand(LoadAttributePage);
            GamePageCommand = new RelayCommand(LoadGamePage);
        }

        /// <summary>
        /// Changes the AccessToken to null and returns then to the login page
        /// </summary>
        private void LogOut()
        {
            AccessToken = null;
            NavigateRequested?.Invoke(new LoginPage());
        }

        /// <summary>
        /// Checks if the user is signed in if
        /// they are then they can navigate to the attribute list page
        /// </summary>
        private void LoadAttributePage()
        {
            if (AccessToken != null)
            {
                NavigateRequested?.Invoke(new UnderConstructionPage());
            }
        }

        /// <summary>
        /// Checks if the user is signed in if
        /// they are then they can navigate to the game list page
        /// </summary>
        private void LoadGamePage()
        {
            if (AccessToken != null)
            {
                NavigateRequested?.Invoke(new GameListPage());
            }
        }
    }
}
