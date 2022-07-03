using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.WPF.Commands;
using CargoLoader.WPF.Controls;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.ViewModels
{
    public class ListingViewModel<T> : ViewModelBase, IListingPageViewModel where T : DomainObject
    {
        private readonly IItemDataService<T> _dataService;
        private readonly ObservableCollection<T> _items;
        private readonly IListingNavigator _listingNavigator;
        private readonly GenericListView _genericListView;
        private int _defaultPageSize = 30;
        private int _page = 1;
        private int _pagesCount;

        public GenericListView GenericListView => _genericListView;
        public IList<IListingPageViewModel> GoodsPages => _listingNavigator.GoodsPages;
        public Type PassedType => typeof(T);
        public IEnumerable<T> Items => _items;
        public int PagesCount
        {
            get
            {
                return _pagesCount;
            }
            set
            {
                _pagesCount = value;
                OnPropertyChanged(nameof(PagesCount));
            }
        }
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            } 
        }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand SpecifiedPageCommand { get; }

        public ListingViewModel(IItemDataService<T> dataService, IListingNavigator listingNavigator)
        {
            _listingNavigator = listingNavigator;
            _dataService = dataService;
            _items = new ObservableCollection<T>();

            _genericListView = new GenericListView(this);
            _genericListView.ItemsSource = _items;

            _listingNavigator.StateChanged += async () => await LoadItemsPage(_page);


            NextPageCommand = new NextListingPageCommand(this);
            PreviousPageCommand = new PreviousListingPageCommand(this);
            SpecifiedPageCommand = new SpecifiedListingPageCommand(this);

            foreach(string enumName in Enum.GetNames<ListingType>())
            {
                if (enumName == typeof(T).Name)
                {
                    GoodsPages.Add(this);
                }
            }
        }

        private async Task LoadItemsPage(int page)
        {
            if(this == _listingNavigator.CurrentListing ) 
            {
                Page = page;
                PagesCount = (await _dataService.GetTableCountAsync() / _defaultPageSize) + 1;

                IEnumerable<T> pageResult = await _dataService.GetPageAsync(page, _defaultPageSize);
                _items.Clear();                

                foreach (T item in pageResult)
                {
                    _items.Add(item);
                }
            }            
        }
        public async Task NextPageAsync()
        {
            if(_page < _pagesCount)
            {
                await LoadItemsPage(_page + 1);
            }
        }

        public async Task PreviousPageAsync()
        {
            if(_page > 1)
            {
                await LoadItemsPage(_page - 1);
            }
        }
        public async Task SpecifiedPageAsync(int requestedPage)
        {
            if(requestedPage > 0 && requestedPage <= _pagesCount)
            {
                await LoadItemsPage(requestedPage);
            }
        }
    }
}
