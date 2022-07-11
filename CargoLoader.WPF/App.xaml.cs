using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork;
using CargoLoader.EntityFraemwork.Services;
using CargoLoader.GalacentreAPI;
using CargoLoader.GalacentreAPI.Models;
using CargoLoader.GalacentreAPI.Services;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                    app.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    string apiKey = context.Configuration.GetValue<string>("GALACENTRE_API_KEY");

                    services.AddSingleton(new GalacentreApiKey(apiKey));
                    services.AddHttpClient<GalacentreHttpClient>(c =>
                    {
                        c.BaseAddress = new Uri("http://www.galacentre.ru/api/v2/catalog/json/");
                    });
                                

                    string connectionString = context.Configuration.GetConnectionString("mssqllocal");
                    services.AddDbContext<CargoLoaderDbContext>(o => o.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CargoLoader;Trusted_Connection=True;"));
                    services.AddSingleton<CargoLoaderDbContextFactory>(new CargoLoaderDbContextFactory(connectionString));


                    services.AddSingleton<ICargoDataService, CargoDataService>();
                    services.AddSingleton<IItemDataService<Product>, ItemDataService<Product>>();
                    services.AddSingleton<IItemDataService<Container>, ItemDataService<Container>>();
                    services.AddSingleton<IOrderDataService, OrderDataService>();

                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<IListingNavigator, ListingNavigator>();

                    services.AddSingleton<OrdersViewModel>();
                    services.AddSingleton<GoodsViewModel>();
                    services.AddSingleton<TransportViewModel>();

                    services.AddSingleton<ListingViewModel<Product>>();
                    services.AddSingleton<ListingViewModel<Container>>();

                    services.AddSingleton<ProductFilterViewModel>();

                    services.AddSingleton<MainViewModel>();

                    //TODO: think about interface or not
                    services.AddSingleton<GalacentreMappingService>();

                    services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();



            _host.Services.GetRequiredService<ListingViewModel<Product>>();
            _host.Services.GetRequiredService<ListingViewModel<Container>>();

            _host.Services.GetRequiredService<ProductFilterViewModel>();


            _host.Services.GetRequiredService<OrdersViewModel>();
            _host.Services.GetRequiredService<GoodsViewModel>();
            _host.Services.GetRequiredService<TransportViewModel>();                      


            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }   
}
