using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataDomain;
using Desktop.ViewModels;
using LogicLayer;
using LogicLayerInterfaces;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, INavigablePage
    {
        private IUserManager _userManager;

        public LoginViewModel ViewModel;
        public event Action<Page> NavigateRequested;
        public event Action<UserVM> UserLoggedIn;

        /// <summary>
        /// Creates a new ViewModel then adds it the DataContext.
        /// Also sets up all requests
        /// </summary>
        /// <param name="userManager"></param>
        public LoginPage(LoginViewModel vm)
        {
            InitializeComponent();

            ViewModel = vm;

            // Requests
            ViewModel.NavigateRequested += page => NavigateRequested?.Invoke(page);
            ViewModel.LoginSucceeded += user => UserLoggedIn?.Invoke(user);
            ViewModel.ClearPasswordRequest += () => pwdPassword.Clear();
            ViewModel.FocusEmailRequest += () => txtEmail.Focus();

            ViewModel.ShowMessageRequest += (message, title) =>
            {
                Window parent = Window.GetWindow(this);
                MessageBox.Show(parent,message, title, 
                                MessageBoxButton.OK,MessageBoxImage.Information);
            };

            DataContext = ViewModel;

            // Display the prefilled password
            pwdPassword.Password = ViewModel.Password;
        }

        /// <summary>
        /// Set the focus to the email box
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Focus();
        }

        /// <summary>
        /// Can't update passowrd directly so everytime a user types in the password box
        /// This runs sending it to the ViewModel
        /// </summary>
        private void pwdPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = pwdPassword.Password;
        }
    }
}
