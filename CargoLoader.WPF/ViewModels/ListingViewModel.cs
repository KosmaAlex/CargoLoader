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
        private Action _instantiationPageLoad;
        private bool _filtersActive;


        public GenericListView GenericListView => _genericListView;
        public IList<IListingPageViewModel> GoodsPages => _listingNavigator.GoodsPages;
        public Type PassedType => typeof(T);
        public IEnumerable<T> Items => _items;

        private int _pagesCount;
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

        private int _page = 1;
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

            //_instantiationPageLoad = async () => await LoadPage(_page);
            //_listingNavigator.StateChanged += _instantiationPageLoad;
            //Task.Run(()  => LoadPage(_page));
            Task.Run(()  => StartLoad());

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

        private void StartLoad()
        {
            Page = 1;
            int tableCount = _dataService.GetTableCountAsync().Result;
            PagesCount = (tableCount + _defaultPageSize - 1) / _defaultPageSize;
            IEnumerable<T> pageResult = _dataService.GetPageAsync(Page, _defaultPageSize).Result;

            foreach (T item in pageResult)
            {
                _items.Add(item);
            }
        }


        private async Task LoadPage(int page)
        {
            if(this == _listingNavigator.CurrentListing ) 
            {
                _filtersActive = false;
                Page = page;
                PagesCount = ((await _dataService.GetTableCountAsync() + _defaultPageSize -1) / _defaultPageSize);

                IEnumerable<T> pageResult = await _dataService.GetPageAsync(page, _defaultPageSize);

                _items.Clear();

                foreach (T item in pageResult)
                {
                    _items.Add(item);
                }

                //_listingNavigator.StateChanged -= _instantiationPageLoad;
                
            }            
        }

        private async Task LoadFilteredPage(int page)
        {
            Page = page;

            (IEnumerable<T> filteredPage, int filtredPageCount) = 
                await _dataService.ExecuteFilteringQuery(page, _defaultPageSize);

            _items.Clear();

            foreach (T item in filteredPage)
            {
                _items.Add(item);
            }
        }

        public async Task ApplyFilters()
        {
            _page = 1;
            _filtersActive = true;

           (IEnumerable<T> filteredPage, int filteredPageCount) = 
                await _dataService.ExecuteFilteringQuery(_page, _defaultPageSize);

            if (filteredPageCount == -1)
            {
                await LoadPage(1);
                return;
            }

            PagesCount = filteredPageCount;

            _items.Clear();

            if (filteredPage.Count() == 0)
            {
                Page = 0;
                return;
            }

            // TODO: decide.
            // If set property up on this method(where now "_page")
            // then, when property changed and nothing to show in listview,
            // "current page property" blinks for a half second as 1 then set to 0
            // to prevent it set property there.
            Page = 1;

            foreach(T item in filteredPage)
            {
                _items.Add(item);
            }
        }

        public async Task NextPageAsync()
        {
            if(_page < _pagesCount)
            {
                if (_filtersActive)
                {
                    await LoadFilteredPage(_page + 1);
                }
                else
                {
                    await LoadPage(_page + 1);
                }
            }
        }

        public async Task PreviousPageAsync()
        {
            if(_page > 1)
            {
                if (_filtersActive)
                {
                    await LoadFilteredPage(_page - 1);
                }
                else
                {
                    await LoadPage(_page - 1);
                }
            }
        }

        public async Task SpecifiedPageAsync(int requestedPage)
        {
            if(requestedPage > 0 && requestedPage <= _pagesCount)
            {
                if (_filtersActive)
                {
                    await LoadFilteredPage(requestedPage);
                }
                else
                {
                    await LoadPage(requestedPage);
                }
            }
        }
    }
}
