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
    public class ProductFiltersViewModel : BaseFiltersViewModel<Product>, IFiltersViewModel
    {
        //private readonly IListingNavigator _listingNavigator;
        //private readonly IItemDataService<Product> _productService;
        
        public Type FiltersType { get; } = typeof(Product);
        public IList<IFiltersViewModel> FiltersCollection => _listingNavigator.FiltersCollection;
        //public ICommand FilteringCommand { get; }


        public ProductFiltersViewModel(IItemDataService<Product> productService, IListingNavigator listingNavigator) 
            : base(productService, listingNavigator)
        {
            //_productService = productService;
            //_listingNavigator = listingNavigator;
            //FilteringCommand = new FilteringCommand(listingNavigator);
            FiltersCollection.Add(this);
        }
    }
}
