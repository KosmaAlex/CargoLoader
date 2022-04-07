using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CargoLoader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {           

            INavigator navigator = new Navigator();

            Window window = new MainWindow();

            new OrdersViewModel(navigator);
            new GoodsViewModel(navigator);
            new TransportViewModel(navigator);

            window.DataContext = new MainViewModel(navigator);
            window.Show();
        }
    }
}
