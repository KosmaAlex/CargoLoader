using CargoLoader.WPF.Commands;
using CargoLoader.WPF.Navigators;
using CargoLoader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigator _navigator;
        public ViewModelBase CurrentView => _navigator.CurrentView;
        public ICommand UpdateCurrentViewCommand { get; }

        public MainViewModel(INavigator navigator, IPageViewModelFactory pageViewModelFactory)
        {
            _navigator = navigator;
            _navigator.StateChanged += Navigator_StateChanged;
            UpdateCurrentViewCommand = new UpdateCurrentViewCommand(navigator, pageViewModelFactory);
            UpdateCurrentViewCommand.Execute(ViewType.Goods);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
