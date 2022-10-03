using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels.Factories
{
    public class PageViewModelFactory : IPageViewModelFactory
    {
        private readonly CreatePageViewModel<OrdersViewModel> _createOrdersViewModel;
        private readonly CreatePageViewModel<GoodsViewModel> _createGoodsViewModel;
        private readonly CreatePageViewModel<TransportViewModel> _createTransportViewModel;

        public PageViewModelFactory(CreatePageViewModel<OrdersViewModel> createOrdersViewModel,
            CreatePageViewModel<GoodsViewModel> createGoodsViewModel,
            CreatePageViewModel<TransportViewModel> createTransportViewModel)
        {
            _createOrdersViewModel = createOrdersViewModel;
            _createGoodsViewModel = createGoodsViewModel;
            _createTransportViewModel = createTransportViewModel;
        }

        public IPageViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Orders:
                    return _createOrdersViewModel();

                case ViewType.Goods:
                    return _createGoodsViewModel();

                case ViewType.Transport:
                    return _createTransportViewModel();

                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", viewType.ToString());
            }
        }
    }
}
