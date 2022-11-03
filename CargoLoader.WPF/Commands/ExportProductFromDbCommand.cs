using CargoLoader.Domain.Models;
using CargoLoader.EntityFraemwork.Services.Export;
using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Commands
{
    public class ExportProductFromDbCommand : ICommand
    {
        private readonly IExportService _exportService;
        private readonly ResourcesViewModel _resourcesViewModel;
        private readonly ExportImportMenuViewModel _exportMenuViewModel;

        public ExportProductFromDbCommand(IExportService exportService, ResourcesViewModel resourcesViewModel)
        {
            _exportService = exportService;
            _resourcesViewModel = resourcesViewModel;
            _exportMenuViewModel = new ExportImportMenuViewModel();
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(_exportMenuViewModel.ProductCheck)
            {
                //_exportService.ExportToCSV<Product>(_resourcesViewModel.ExportFolder);
            }
            if (_exportMenuViewModel.ContainerCheck)
            {
                //_exportService.ExportToCSV<Container>(_resourcesViewModel.ExportFolder);
            }
            
        }
    }
}
