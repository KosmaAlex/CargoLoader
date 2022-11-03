using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork;
using CargoLoader.EntityFraemwork.Services;
using CargoLoader.EntityFraemwork.Services.Common;
using CargoLoader.EntityFraemwork.Services.Export;
using CargoLoader.GalacentreAPI;
using CargoLoader.GalacentreAPI.Models;
using CargoLoader.GalacentreAPI.Services;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
using CargoLoader.WPF.ViewModels.Factories;
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
                    services.AddHttpClient<GalacentreHttpService>(c =>
                    {
                        c.BaseAddress = new Uri("http://www.galacentre.ru/");
                    });                             

                    string connectionString = context.Configuration.GetConnectionString("mssqllocal");
                    services.AddDbContext<CargoLoaderDbContext>(o => o.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CargoLoader;Trusted_Connection=True;"));
                    services.AddSingleton<CargoLoaderDbContextFactory>(new CargoLoaderDbContextFactory(connectionString));

                    //services.AddSingleton<ICargoDataService, CargoDataService>();
                    services.AddSingleton<IItemDataService<Product>, ItemDataService<Product>>();
                    services.AddSingleton<IItemDataService<Container>, ItemDataService<Container>>();
                    //services.AddSingleton<IOrderDataService, OrderDataService>();

                    services.AddSingleton<IExportService, ExportService>();
                    //services.AddSingleton<IExportService<Container>, ExportService<Container>>();

                    //
                    services.AddSingleton<IDataService, NonQueryDataService>();
                    //


                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<IListingNavigator, ListingNavigator>();

                    services.AddSingleton<OrdersViewModel>();
                    services.AddSingleton<GoodsViewModel>();
                    services.AddSingleton<TransportViewModel>();
                    services.AddSingleton<ResourcesViewModel>();

                    services.AddSingleton<IPageViewModelFactory, PageViewModelFactory>();
                    services.AddSingleton<CreatePageViewModel<OrdersViewModel>>(services => 
                    () => services.GetRequiredService<OrdersViewModel>());
                    services.AddSingleton<CreatePageViewModel<GoodsViewModel>>(services => 
                    () => services.GetRequiredService<GoodsViewModel>());
                    services.AddSingleton<CreatePageViewModel<TransportViewModel>>(services => 
                    () => services.GetRequiredService<TransportViewModel>());
                    services.AddSingleton<CreatePageViewModel<ResourcesViewModel>>(services =>
                    () => services.GetRequiredService<ResourcesViewModel>());


                    services.AddSingleton<ListingViewModel<Product>>();
                    services.AddSingleton<ListingViewModel<Container>>();

                    services.AddSingleton<IListingPageViewModelFactory, ListingPageViewModelFactory>();
                    services.AddSingleton<CreateListingPageViewModel<ListingViewModel<Product>>>(services =>
                    () => services.GetRequiredService<ListingViewModel<Product>>());
                    services.AddSingleton<CreateListingPageViewModel<ListingViewModel<Container>>>(services =>
                    () => services.GetRequiredService<ListingViewModel<Container>>());
                                                          

                    services.AddSingleton<ProductFiltersViewModel>();
                    services.AddSingleton<ContainerFiltersViewModel>();

                    services.AddSingleton<IFiltersViewModelFactory, FiltersViewModelFactory>();
                    services.AddSingleton<CreateFiltersViewModel<ProductFiltersViewModel>>(services =>
                    () => services.GetRequiredService<ProductFiltersViewModel>());
                    services.AddSingleton<CreateFiltersViewModel<ContainerFiltersViewModel>>(services =>
                    () => services.GetRequiredService<ContainerFiltersViewModel>());

                    services.AddSingleton<MainViewModel>();

                    //TODO: think about interface or not
                    services.AddSingleton<GalacentreMappingService>();


                    services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();



            //_host.Services.GetRequiredService<ListingViewModel<Product>>();
            //_host.Services.GetRequiredService<ListingViewModel<Container>>();

            //_host.Services.GetRequiredService<ProductFiltersViewModel>();
            //_host.Services.GetRequiredService<ContainerFiltersViewModel>();


            //_host.Services.GetRequiredService<OrdersViewModel>();
            //_host.Services.GetRequiredService<GoodsViewModel>();
            //_host.Services.GetRequiredService<TransportViewModel>();                      


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
