using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services;
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
        public IList<IPageViewModel> Pages => _navigator.Pages;
        public ICommand UpdateCurrentListingCommand { get; }

        public GoodsViewModel(INavigator navigator, IListingNavigator listingNavigator,
            IItemDataService<Product> dataService)
        {
            _navigator = navigator;
            _listingNavigator = listingNavigator;
            _listingNavigator.StateChanged += ListingNavigator_StateChanged;
            UpdateCurrentListingCommand = new UpdateCurrentListingCommand(listingNavigator);
            UpdateCurrentListingCommand.Execute(ListingType.Container);
            Pages.Add(this);
        }

        private void ListingNavigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentListing));
        }
    }
}
