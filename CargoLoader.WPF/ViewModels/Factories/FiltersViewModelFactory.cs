using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels.Factories
{
    public class FiltersViewModelFactory : IFiltersViewModelFactory
    {
        private readonly CreateFiltersViewModel<ProductFiltersViewModel> _createProductFilters;
        private readonly CreateFiltersViewModel<ContainerFiltersViewModel> _createContainerFilters;

        public FiltersViewModelFactory(CreateFiltersViewModel<ProductFiltersViewModel> createProductFilters,
            CreateFiltersViewModel<ContainerFiltersViewModel> createContainerFilters)
        {
            _createProductFilters = createProductFilters;
            _createContainerFilters = createContainerFilters;
        }

        public IFiltersViewModel CreateFilterViewModel(ListingType listingType)
        {
            switch (listingType)
            {
                case ListingType.Product:
                    return _createProductFilters();

                case ListingType.Container:
                    return _createContainerFilters();

                default:
                    throw new ArgumentException("The ListingType does not have FiltersModel", listingType.ToString());
            }
        }
    }
}
