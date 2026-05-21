using System.Configuration;
using System.Data;
using System.Windows;
using Desktop.ViewModels;
using Desktop.Views;
using Desktop.Views.Pages;
using Desktop.Views.Windows;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }

        /// <summary>
        /// Sets the IServiceProvider and loads the MainWindow
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            Services = serviceCollection.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Saves all Managers, VMs, Windows, and Pages into a ServiceCollection
        /// so they can be referenced later
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServices(IServiceCollection services)
        {
            // Managers
            services.AddSingleton<IUserManager, UserManager>();
            services.AddSingleton<IGameManager, GameManager>();

            // ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<GameListViewModel>();
            services.AddTransient<GameAddViewModel>();

            // Windows
            services.AddTransient<MainWindow>(); 
            services.AddTransient<GameManageWindow>();

            // Pages
            services.AddTransient<UnderConstructionPage>();
            services.AddTransient<LoginPage>();
            services.AddTransient<GameListPage>();
        }
    }

}
