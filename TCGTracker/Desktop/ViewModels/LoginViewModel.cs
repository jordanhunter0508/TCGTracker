using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataDomain;
using Desktop.Views.Pages;
using LogicLayerInterfaces;

namespace Desktop.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager _userManager;
        private readonly Window _owner = Application.Current.MainWindow;

        private string _email;
        private string _password;
        private string _errorMessage;

        public UserVM AccessToken { get; private set; }
        public event Action<UserVM> LoginSucceeded;

        // Requests
        public event Action<Page> NavigateRequested;
        public event Action ClearPasswordRequest;
        public event Action FocusEmailRequest;
        public event Action<string, string> ShowMessageRequest;

        // Commands
        public ICommand LogInCommand { get; }

        // View properties
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                (LogInCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                (LogInCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Used to display an error if a field is not filled out
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
        /// Sets the command to log in and prefills with the system admins credentials.
        /// </summary>
        public LoginViewModel(IUserManager userManager)
        {
            _userManager = userManager;

            LogInCommand = new RelayCommand(LogIn, CanLogIn);

            //AUTO FILL LOG IN
            _email = "john@mail.com";
            _password = "newuser";
        }

        /// <summary>
        /// Attempt to log the user in. 
        /// If the user is not a system admin it logs them out and displays an error message. <br/>
        /// Takes the user to the attributes list page.
        /// </summary>
        private void LogIn()
        {
            if (!IsValid())
            {
                return;
            }

            try
            {
                AccessToken = _userManager.LoginUser(_email, _password);

                if (AccessToken != null && AccessToken.Roles.Contains("System_Admin"))
                {
                    LoginSucceeded?.Invoke(AccessToken);
                    NavigateRequested?.Invoke(new UnderConstructionPage());
                }
                else if (AccessToken != null && !AccessToken.Roles.Contains("System_Admin"))
                {
                    string invalidRoleMessage = "This application is only for system admins.\n" +
                                                "If you have a system admin account please use those credentials.";
                    ShowMessageRequest?.Invoke(invalidRoleMessage, "Invalid Role");
                }
                else
                {
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(_owner,ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Validates the user's input
        /// </summary>
        /// <returns>Returns false if the user input is invalid, true otherwise</returns>
        private bool IsValid()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(_email))
            {
                isValid = false;
            }
            else if (string.IsNullOrWhiteSpace(_password))
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Helps the display the button if all input boxes are filled out
        /// </summary>
        /// <returns>Returns false if there is a validation error, true otherwise</returns>
        private bool CanLogIn()
        { 
            bool canLogIn = true; 
            if (string.IsNullOrWhiteSpace(_email))
            {
                canLogIn = false;
            }
            else if (string.IsNullOrWhiteSpace(_password))
            {
                canLogIn = false;
            }

            return canLogIn;
        }

        /// <summary>
        /// Clears all inputs and displays an invalid credentials message
        /// </summary>
        private void ClearInputs()
        {
            Email = "";
            Password = "";
            ErrorMessage = "Invalid email or password.";

            ClearPasswordRequest?.Invoke();
            FocusEmailRequest?.Invoke();
        }
    }
}
