using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services.Export;
using CargoLoader.GalacentreAPI.Services;
using CargoLoader.WPF.Commands;
using CargoLoader.WPF.Controls;
using CargoLoader.WPF.Navigators;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.ViewModels
{
    public class ResourcesViewModel : ViewModelBase, IPageViewModel
    {   
        private readonly INavigator _navigation;
        public IList<IPageViewModel> Pages => _navigation.Pages;

        private readonly ExportImportMenuViewModel _eiMenuViewModel;
        public ExportImportMenuViewModel EIMenuViewModel => _eiMenuViewModel;


        public ICommand GetDataToDbCommand { get; }
        public ICommand GetContainersCommand { get; }
        public ICommand ExportProductFromDbCommand { get; }
        public ICommand ExportContainerFromDbCommand { get; }
        public ICommand BrowseFolderCommand { get;}

        public ResourcesViewModel(INavigator navigator, GalacentreMappingService mappingService, IDataService service,
            IExportService exportService)
        {
            _navigation = navigator;            
            GetDataToDbCommand = new GetDataToDbCommand(mappingService);
            GetContainersCommand = new GetContainersCommand(service);
            ExportProductFromDbCommand = new ExportProductFromDbCommand(exportService, this);
            ExportContainerFromDbCommand = new ExportContainerFromDbCommand(exportService, this);
            BrowseFolderCommand = new BrowseFolderCommand(this);
        }
    }
}
