using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels.Factories
{
    public interface IPageViewModelFactory
    {
        IPageViewModel CreateViewModel(ViewType viewType);
    }
}
