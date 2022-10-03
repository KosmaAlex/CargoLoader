using CargoLoader.WPF.Controls;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
using CargoLoader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Commands
{
    public class UpdateCurrentListingCommand : ICommand
    {
        private readonly IListingNavigator _listingNavigator;
        private readonly IListingPageViewModelFactory _listingFactory;
        private readonly IFiltersViewModelFactory _filtersFactory;

        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentListingCommand(IListingNavigator listingNavigator,
            IListingPageViewModelFactory listingFactory, IFiltersViewModelFactory filtersFactory)
        {
            _listingNavigator = listingNavigator;
            _listingFactory = listingFactory;
            _filtersFactory = filtersFactory;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is ListingType)
            {
                ListingType listingType = (ListingType)parameter;

                var listing = _listingNavigator.GoodsPages
                    .FirstOrDefault(l => l.GetType().GetGenericArguments()[0].Name == listingType.ToString());                

                if(listing == null)
                {
                    listing = _listingFactory.CreatePageViewModel(listingType);
                }

                var filters = _listingNavigator.FiltersCollection
                    .FirstOrDefault(f => f.FiltersType == listing.GetType().GetGenericArguments()[0]);

                if(filters == null)
                {
                    filters = _filtersFactory.CreateFilterViewModel(listingType);
                }

                _listingNavigator.CurrentListing = listing;
                _listingNavigator.CurrentFilters = filters;
            }
        }
    }
}
