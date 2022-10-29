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
    public class ExportContainerFromDbCommand : ICommand
    {
        private readonly IExportService<Container> _exportService;
        private readonly ResourcesViewModel _resourcesViewModel;

        public ExportContainerFromDbCommand(IExportService<Container> exportService, ResourcesViewModel resourcesViewModel)
        {
            _exportService = exportService;
            _resourcesViewModel = resourcesViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _exportService.ExportToCSV(_resourcesViewModel.ExportFolder);
        }
    }
}
