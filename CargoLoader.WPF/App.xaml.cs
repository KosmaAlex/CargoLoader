using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            //INavigator navigator = new Navigator();

            //new OrdersViewModel(navigator);
            //new GoodsViewModel(navigator);
            //new TransportViewModel(navigator);

            _host.Services.GetRequiredService<OrdersViewModel>();
            _host.Services.GetRequiredService<GoodsViewModel>();
            _host.Services.GetRequiredService<TransportViewModel>();

            Window window = _host.Services.GetRequiredService<MainWindow>();
            //window.DataContext = new MainViewModel(_host.Services.GetRequiredService<INavigator>());
            window.Show();
        }

        static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ICargoDataService, CargoDataService>();
                    services.AddSingleton<IItemDataService<Product>, ItemDataService<Product>>();
                    services.AddSingleton<IItemDataService<Container>, ItemDataService<Container>>();
                    services.AddSingleton<IOrderDataService, OrderDataService>();

                    services.AddSingleton<INavigator, Navigator>();

                    services.AddSingleton<OrdersViewModel>();
                    services.AddSingleton<GoodsViewModel>();
                    services.AddSingleton<TransportViewModel>();

                    services.AddSingleton<MainViewModel>();

                    services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });



        }        
    }
}
