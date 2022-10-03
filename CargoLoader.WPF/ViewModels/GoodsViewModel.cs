using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services;
using CargoLoader.GalacentreAPI.Services;
using CargoLoader.WPF.Commands;
using CargoLoader.WPF.Controls;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.ViewModels
{
    public delegate TViewModel CreateListingPageViewModel<TViewModel>() where TViewModel : IListingPageViewModel;
    public delegate TViewModel CreateFiltersViewModel<TViewModel>() where TViewModel : IFiltersViewModel;

    public class GoodsViewModel : ViewModelBase, IPageViewModel
    {
        private readonly INavigator _navigator;
        private readonly IListingNavigator _listingNavigator;

        public IListingPageViewModel CurrentListing => _listingNavigator.CurrentListing;
        public IFiltersViewModel CurrentFilters => _listingNavigator.CurrentFilters;
        public IList<IPageViewModel> Pages => _navigator.Pages;
        public ICommand UpdateCurrentListingCommand { get; }
        public ICommand GetDataToDbCommand { get; }
        public ICommand GetContainersCommand { get; }

        public GoodsViewModel(INavigator navigator, IListingNavigator listingNavigator,
            IItemDataService<Product> dataService, GalacentreMappingService mappingService,
            IItemDataService<Container> containerService, IListingPageViewModelFactory listingFactory,
            IFiltersViewModelFactory filtersFactory)
        {
            _navigator = navigator;
            _listingNavigator = listingNavigator;
            _listingNavigator.StateChanged += ListingNavigator_StateChanged;
            _listingNavigator.StateChanged += ListingFilters_StateChanged;
            GetDataToDbCommand = new GetDataToDbCommand(mappingService);
            GetContainersCommand = new GetContainersCommand(containerService);
            UpdateCurrentListingCommand = new UpdateCurrentListingCommand(listingNavigator, listingFactory, filtersFactory);
            Pages.Add(this);
        }

        private void ListingNavigator_StateChanged()
        {            
            OnPropertyChanged(nameof(CurrentListing));
        }

        private void ListingFilters_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentFilters));
        }
    }
}
