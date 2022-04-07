using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels;
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
        private INavigator _navigator;
        
        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentViewCommand(INavigator navigator)
        {
            _navigator = navigator;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
         {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentView = (ViewModelBase)_navigator.Pages
                    .FirstOrDefault(v => v.GetType().Name.Replace("ViewModel", "") == viewType.ToString());
            }
        }
    }
}
