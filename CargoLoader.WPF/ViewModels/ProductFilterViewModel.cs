using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.WPF.Commands;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.ViewModels
{
    public class ProductFilterViewModel : ViewModelBase, IFiltersViewModel
    {
        private readonly IListingNavigator _listingNavigator;
        private readonly IItemDataService<Product> _productService;
        
        public Type FiltersType { get; } = typeof(Product);
        public IList<IFiltersViewModel> FiltersCollection => _listingNavigator.FiltersCollection;
        public ICommand FilteringCommand { get; }


        public ProductFilterViewModel(IListingNavigator listingNavigator, IItemDataService<Product> productService)
        {
            _productService = productService;
            _listingNavigator = listingNavigator;
            FilteringCommand = new FilteringCommand(listingNavigator);
            FiltersCollection.Add(this);
        }

        #region Properties
        private string? _marking;
        public string? Marking
        {
            get
            {
                return _marking;
            }
            set
            {
                _marking = value;
                _productService.QueryByMarking(_marking);
                OnPropertyChanged(nameof(Marking));
            }
        }

        private string? _name;
        public string? Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                _productService.QueryByName(_name);
                OnPropertyChanged(nameof(Name));
            }
        }

        private decimal? _width;
        public decimal? Width
        {
            get
            { 
                return _width;
            }
            set
            {
                _width = value;
                _productService.QueryByWidth(_width, _minWidth);
                OnPropertyChanged(nameof(Width));
            }
        }

        private decimal? _minWidth;

        public decimal? MinWidth
        {
            get
            {
                return _minWidth;
            }
            set
            {
                _minWidth = value;
                _productService.QueryByWidth(_width, _minWidth);
                OnPropertyChanged(nameof(MinWidth));
            }
        }

        private decimal? _length;
        public decimal? Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                _productService.QueryByLength(_length, _minLength);
                OnPropertyChanged(nameof(Length));
            }
        }


        private decimal? _minLength;
        public decimal? MinLength
        {
            get
            {
                return _minLength;
            }
            set
            {
                _minLength = value;
                _productService.QueryByLength(_length, _minLength);
                OnPropertyChanged(nameof(MinLength));
            }
        }


        private decimal? _height;
        public decimal? Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                _productService.QueryByHeight(_height, _minHeight);
                OnPropertyChanged(nameof(Height));                
            }
        }


        private decimal? _minHeight;
        public decimal? MinHeight
        {
            get
            {
                return _minHeight;
            }
            set
            {
                _minHeight = value;
                _productService.QueryByHeight(_height, _minHeight);
                OnPropertyChanged(nameof(MinHeight));
            }
        }


        private decimal? _volume;
        public decimal? Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                _productService.QueryByVolume(_volume, _minVolume);
                OnPropertyChanged(nameof(Volume));
            }
        }

        private decimal? _minVolume;
        public decimal? MinVolume
        {
            get
            {
                return _minVolume;
            }
            set
            {
                _minVolume = value;
                _productService.QueryByVolume(_volume, _minVolume);
                OnPropertyChanged(nameof(MinVolume));
            }
        }

        private decimal? _weight;
        public decimal? Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                _productService.QueryByWeight(_weight, _minWeight);
                OnPropertyChanged(nameof(Weight));
            }
        }


        private decimal? _minWeight;
        public decimal? MinWeight 
        {
            get
            {
                return _minWeight;
            }
            set
            {
                _minWeight = value;
                _productService.QueryByWeight(_weight,_minWeight);
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
                _productService.QueryByIsFragile(_isFragile);
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
                _productService.QueryByIsRotatable(_isRotatable);
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
                _productService.QueryByIsProp(_isProp);
                OnPropertyChanged(nameof(IsProp));
            }
        }


        private bool? _isContainer;
        public bool? IsContainer
        {
            get
            {
                return _isContainer;
            }
            set
            {
                _isContainer = value;
                _productService.QueryByIsContainer(_isContainer);
                OnPropertyChanged(nameof(IsContainer));
            }
        }

        //TODO: find another way cast to null values 'Is' properties
        private bool _fragileCheck;
        public bool FragileCheck
        {
            get
            {
                return _fragileCheck;
            }
            set
            {
                _fragileCheck = value;
                if (value)
                {
                    IsFragile = false;
                }
                if (!value)
                {
                    IsFragile = null;
                }
                OnPropertyChanged(nameof(FragileCheck));
            }
        }

        private bool _potCheck;
        public bool POTCheck
        {
            get
            {
                return _potCheck;
            }
            set
            {
                _potCheck = value;
                if (value)
                {
                    IsProp = false;
                }
                if (!value)
                {
                    IsProp = null;
                }
                OnPropertyChanged(nameof(POTCheck));
            }
        }

        private bool _rotateCheck;
        public bool RotateCheck
        {
            get
            {
                return _rotateCheck;
            }
            set
            {
                _rotateCheck = value;
                if (value)
                {
                    IsRotatable = false;
                }
                if (!value)
                {
                    IsRotatable = null;
                }
                OnPropertyChanged(nameof(RotateCheck));
            }
        }
        #endregion



    }
}
