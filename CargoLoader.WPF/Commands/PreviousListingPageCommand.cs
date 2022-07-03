using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Commands
{
    public class PreviousListingPageCommand : ICommand
    {
        private readonly IListingPageViewModel _listing;

        public event EventHandler? CanExecuteChanged;

        public PreviousListingPageCommand(IListingPageViewModel listing)
        {
            _listing = listing;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _listing.PreviousPageAsync();
        }
    }
}
