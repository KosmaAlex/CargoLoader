using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services;
using CargoLoader.GalacentreAPI.Services;
using CargoLoader.WPF.Commands;
using CargoLoader.WPF.Controls;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.ViewModels
{
    public class GoodsViewModel : ViewModelBase, IPageViewModel
    {
        private readonly INavigator _navigator;
        private readonly IListingNavigator _listingNavigator;

        public IListingPageViewModel CurrentListing => _listingNavigator.CurrentListing;
        public IFiltersViewModel CurrentFilters => _listingNavigator.CurrentFilters;
        public IList<IPageViewModel> Pages => _navigator.Pages;
        public ICommand UpdateCurrentListingCommand { get; }
        public ICommand GetDataToDbCommand { get; }

        public GoodsViewModel(INavigator navigator, IListingNavigator listingNavigator,
            IItemDataService<Product> dataService, GalacentreMappingService mappingService)
        {
            _navigator = navigator;
            _listingNavigator = listingNavigator;
            _listingNavigator.StateChanged += ListingNavigator_StateChanged;
            _listingNavigator.StateChanged += ListingFilters_StateChanged;
            GetDataToDbCommand = new GetDataToDbCommand(mappingService);
            UpdateCurrentListingCommand = new UpdateCurrentListingCommand(listingNavigator);
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
