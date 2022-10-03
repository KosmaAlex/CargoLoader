using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
using CargoLoader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Commands
{
    internal class UpdateCurrentViewCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IPageViewModelFactory _pageFactory;
        
        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentViewCommand(INavigator navigator, IPageViewModelFactory pageViewModelFactory)
        {
            _navigator = navigator;
            _pageFactory = pageViewModelFactory;

        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                var page = (ViewModelBase)_navigator.Pages
                    .FirstOrDefault(v => v.GetType().Name.Replace(Constants.ViewModelTrim, string.Empty) == viewType.ToString());

                if(page == null)
                {
                    page = (ViewModelBase)_pageFactory.CreateViewModel(viewType);
                }

                _navigator.CurrentView = page;
            }
        }
    }
}
