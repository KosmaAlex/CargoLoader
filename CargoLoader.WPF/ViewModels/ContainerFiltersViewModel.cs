using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services.ExtensionMethods;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public class ContainerFiltersViewModel : BaseFiltersViewModel<Container>, IFiltersViewModel
    {
        public Type FiltersType { get; } = typeof(Container);
        public IList<IFiltersViewModel> FiltersCollection => _listingNavigator.FiltersCollection;

        public ContainerFiltersViewModel(IItemDataService<Container> dataService, IListingNavigator listingNavigator)
            : base(dataService, listingNavigator)
        {
            FiltersCollection.Add(this);
        }


        private decimal? _minCapacity;
        public decimal? MinCapacity
        {
            get
            {
                return _minCapacity;
            }
            set
            {
                _minCapacity = value;
                _dataService.QueryByCapacity(Capacity, MinCapacity);
                OnPropertyChanged(nameof(MinCapacity));
            }
        }

        private decimal? _capacity;
        public decimal? Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                _capacity = value;
                _dataService.QueryByCapacity(Capacity, MinCapacity);
                OnPropertyChanged(nameof(Capacity));
            }
        }


        private bool _thumbnail;
        public bool  Thumbnail
        {
            get
            {
                return _thumbnail;
            }
            set
            {
                _thumbnail = value;
                OnPropertyChanged(nameof(Thumbnail));
            }
        }

    }
}
