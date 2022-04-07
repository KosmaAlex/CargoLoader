using CargoLoader.WPF.Commands;
using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }

            set
            {
                _currentView = value;
                StateChanged?.Invoke();
            }
        }
        private readonly IList<IPageViewModel> _pages;
        public IList<IPageViewModel> Pages
        {
            get { return _pages; }
        }

        public event Action StateChanged;

        public Navigator()
        {
            _pages = new List<IPageViewModel>();
        }

    }
}
