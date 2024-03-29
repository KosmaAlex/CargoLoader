﻿using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public class OrdersViewModel : ViewModelBase, IPageViewModel
    {
        private readonly INavigator _navigator;
        public IList<IPageViewModel> Pages => _navigator.Pages;

        public OrdersViewModel(INavigator navigator)
        {
            _navigator = navigator;
            Pages.Add(this);
        }
    }
}
