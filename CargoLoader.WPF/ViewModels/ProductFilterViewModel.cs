using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public class ProductFilterViewModel : ViewModelBase, IFiltersViewModel
    {
        private readonly IListingNavigator _listing;
        private readonly IItemDataService<Product> _productService;
        
        public Type FiltersType { get; } = typeof(Product);
        public IList<IFiltersViewModel> FiltersCollection => _listing.FiltersCollection;


        public ProductFilterViewModel(IListingNavigator listing, IItemDataService<Product> productService)
        {
            _productService = productService;
            _listing = listing;
            FiltersCollection.Add(this);
        }

        #region Properties
        private string _marking;
        public string Marking
        {
            get
            {
                return _marking;
            }
            set
            {
                _marking = value;
                OnPropertyChanged(nameof(Marking));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private decimal _width;
        public decimal Width
        {
            get
            { 
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private decimal _minWidth;

        public decimal MinWidth
        {
            get
            {
                return _minWidth;
            }
            set
            {
                _minWidth = value;
                OnPropertyChanged(nameof(MinWidth));
            }
        }

        private decimal _length;
        public decimal Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                OnPropertyChanged(nameof(Length));
            }
        }


        private decimal _minLength;
        public decimal MinLength
        {
            get
            {
                return _minLength;
            }
            set
            {
                _minLength = value;
                OnPropertyChanged(nameof(MinLength));
            }
        }


        private decimal _height;
        public decimal Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }


        private decimal _minHeight;
        public decimal MinHeight
        {
            get
            {
                return _minHeight;
            }
            set
            {
                _minHeight = value;
                OnPropertyChanged(nameof(MinHeight));
            }
        }


        private decimal _volume;
        public decimal Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        private decimal _minVolume;
        public decimal MinVolume
        {
            get
            {
                return _minVolume;
            }
            set
            {
                _minVolume = value;
                OnPropertyChanged(nameof(MinVolume));
            }
        }

        private decimal _weight;
        public decimal Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }


        private decimal _minWeight;
        public decimal MinWeight 
        {
            get
            {
                return _minWeight;
            }
            set
            {
                _minWeight = value;
                OnPropertyChanged(nameof(MinWeight));
            }
        }

        private bool? _isFragile;
        public bool? IsFragile
        {
            get
            {
                return _isFragile;
            }
            set
            {
                _isFragile = value;
                OnPropertyChanged(nameof(IsFragile));
            }
        }
                
        private bool? _isRotatable;
        public bool? IsRotatable
        {
            get
            {
                return _isRotatable;
            }
            set
            {
                _isRotatable = value;
                OnPropertyChanged(nameof(IsRotatable));
            }
        }


        private bool? _isProp;
        public bool? IsProp
        {
            get
            {
                return _isProp;
            }
            set
            {
                _isProp = value;
                OnPropertyChanged(nameof(IsProp));
            }
        }


        private bool _isContainer;
        public bool IsContainer
        {
            get
            {
                return _isContainer;
            }
            set
            {
                _isContainer = value;
                OnPropertyChanged(nameof(IsContainer));
            }
        }
        #endregion

        

    }
}
