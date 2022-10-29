using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.Navigators
{
    public enum ViewType
    {
        Orders,
        Goods,
        Transport,
        Resources
    }

    public interface INavigator
    {
        ViewModelBase CurrentView { get; set; }
        IList<IPageViewModel> Pages { get; }

        event Action StateChanged;
    }
}
