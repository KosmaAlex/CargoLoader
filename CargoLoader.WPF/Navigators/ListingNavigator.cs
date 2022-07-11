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
                _currentFilters = _filtersCollection.FirstOrDefault(f => 
                    f.FiltersType == _currentListing.GetType().GetGenericArguments()[0]);
                StateChanged.Invoke();
            }
        }
        private readonly IList<IListingPageViewModel> _goodsPages;
        public IList<IListingPageViewModel> GoodsPages
        {
            get { return _goodsPages; }
        }

        private IFiltersViewModel _currentFilters;
        public IFiltersViewModel CurrentFilters => _currentFilters;

        private readonly IList<IFiltersViewModel> _filtersCollection;
        public IList<IFiltersViewModel> FiltersCollection => _filtersCollection;


        public event Action StateChanged;

        public ListingNavigator()
        {
            _goodsPages = new List<IListingPageViewModel>();
            _filtersCollection = new List<IFiltersViewModel>();
        }
    }
}
