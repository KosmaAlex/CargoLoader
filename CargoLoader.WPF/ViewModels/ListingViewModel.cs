using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public class ListingViewModel<T> : ViewModelBase, IListingPageViewModel where T : DomainObject
    {
        private readonly IItemDataService<T> _dataService;
        private readonly ObservableCollection<T> _items;
        private readonly IListingNavigator _listingNavigator;

        public IList<IListingPageViewModel> GoodsPages => _listingNavigator.GoodsPages;
        public Type PassedType => typeof(T);
        public IEnumerable<T> Items => _items;

        public ListingViewModel(IItemDataService<T> dataService, IListingNavigator listingNavigator)
        {
            _listingNavigator = listingNavigator;
            _dataService = dataService;
            _items = new ObservableCollection<T>();

            foreach(string enumName in Enum.GetNames<ListingType>())
            {
                if (enumName == typeof(T).Name)
                {
                    GoodsPages.Add(this);
                }
            }

            LoadItems();
        }

        private async Task LoadItems()
        {
            IEnumerable<T> loadItems = await _dataService.GetAll();
            foreach (T item in loadItems)
            {
                _items.Add(item);
            }
        }
    }
}
