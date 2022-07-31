using CargoLoader.Domain.Services;
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
    public class FilteringCommand : ICommand
    {
        private readonly IListingNavigator _listingNav;
        
        public event EventHandler? CanExecuteChanged;

        public FilteringCommand(IListingNavigator listingNav)
        {
            _listingNav = listingNav;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _listingNav.CurrentListing.ApplyFilters();
        }
    }
}
