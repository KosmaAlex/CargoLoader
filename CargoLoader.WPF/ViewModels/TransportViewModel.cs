using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public class TransportViewModel : ViewModelBase, IPageViewModel
    {
        private readonly INavigator _navigator;
        public IList<IPageViewModel> Pages => _navigator.Pages;

        public TransportViewModel(INavigator navigator)
        {
            _navigator = navigator;
            Pages.Add(this);

            
        }

        public List<byte[]> images { get; set; }
        
    }
}
