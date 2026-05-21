using System.Text;
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
using Desktop.Views.Pages;
using Desktop.Views.Windows;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserManager _userManager;
        private MainViewModel _vm;

        /// <summary>
        /// Initializes the page content then sets the
        /// DataContext to this pages ViewModel
        /// </summary>
        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();

            _vm = vm;
            _vm.NavigateRequested += NavigateToPage;
            DataContext = _vm;
        }

        /// <summary>
        /// Loads the LoginPage when the window fully loads
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginPage page = ((App)Application.Current).Services
                                                .GetRequiredService<LoginPage>();
            NavigateToPage(page);
        }

        /// <summary>
        /// Navigate the user to the page passed in
        /// </summary>
        private void NavigateToPage(Page page)
        {
            if (page is INavigablePage navigablePage)
            {
                navigablePage.NavigateRequested += NavigateToPage;
                navigablePage.UserLoggedIn += user => _vm.AccessToken = user;
            }

            frmMain.Navigate(page);
        }
    }
}