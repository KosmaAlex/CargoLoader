using CargoLoader.WPF.Controls;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
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

        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentListingCommand(IListingNavigator listingNavigator)
        {
            _listingNavigator = listingNavigator;
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

                _listingNavigator.CurrentListing = _listingNavigator.GoodsPages.FirstOrDefault(l =>
                l.GetType().GetGenericArguments()[0].Name == listingType.ToString());
            }
        }
    }
}
