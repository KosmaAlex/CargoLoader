using CargoLoader.Domain.Models;
using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.Navigators
{
    public enum ListingType
    {
        Product,
        Container
    }
    public interface IListingNavigator
    {
        IListingPageViewModel CurrentListing { get; set; }
        IList<IListingPageViewModel> GoodsPages { get; }

        IFiltersViewModel CurrentFilters { get; }
        IList<IFiltersViewModel> FiltersCollection { get; }

        event Action StateChanged;
    }
}
