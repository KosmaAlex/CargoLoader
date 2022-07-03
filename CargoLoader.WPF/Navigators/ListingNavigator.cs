using CargoLoader.Domain.Models;
using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.Navigators
{
    public class ListingNavigator : IListingNavigator
    {
        private IListingPageViewModel _currentListing;
        public IListingPageViewModel CurrentListing
        {
            get { return _currentListing; }
            set
            {
                _currentListing = value;
                StateChanged.Invoke();
            }
        }
        private readonly IList<IListingPageViewModel> _goodsPages;
        public IList<IListingPageViewModel> GoodsPages
        {
            get { return _goodsPages; }
        }

        public event Action StateChanged;

        public ListingNavigator()
        {
            _goodsPages = new List<IListingPageViewModel>();
        }
    }
}
