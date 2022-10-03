using CargoLoader.Domain.Models;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels.Factories
{
    public class ListingPageViewModelFactory : IListingPageViewModelFactory
    {
        public readonly CreateListingPageViewModel<ListingViewModel<Product>> _createProductListing;
        public readonly CreateListingPageViewModel<ListingViewModel<Container>> _createContainerListing;

        public ListingPageViewModelFactory(CreateListingPageViewModel<ListingViewModel<Product>> createProductListing,
            CreateListingPageViewModel<ListingViewModel<Container>> createContainerListing)
        {
            _createProductListing = createProductListing;
            _createContainerListing = createContainerListing;
        }

        public IListingPageViewModel CreatePageViewModel(ListingType listingType)
        {
            switch (listingType)
            {
                case ListingType.Product:
                    return _createProductListing();

                case ListingType.Container:
                    return _createContainerListing();

                default:
                    throw new ArgumentException("The ListingType does not have a ListingModel", listingType.ToString());
            }
        }
    }
}
