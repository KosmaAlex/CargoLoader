using CargoLoader.EntityFraemwork.Services.Export;
using CargoLoader.WPF.ViewModels;
using Microsoft.VisualBasic.ApplicationServices;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CargoLoader.WPF.Commands
{
    public class BrowseFolderCommand : ICommand
    {
        private readonly ResourcesViewModel _viewModel;
        private readonly VistaFolderBrowserDialog _dialog;

        public BrowseFolderCommand(ResourcesViewModel viewModel)
        {
            _viewModel = viewModel;
            _dialog = new VistaFolderBrowserDialog();
            _viewModel.ExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + '\\';
            _dialog.SelectedPath = _viewModel.ExportFolder;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            
            if (_dialog.ShowDialog() == true)
            {
                _dialog.SelectedPath = _dialog.SelectedPath + "\\";
                _viewModel.ExportFolder = _dialog.SelectedPath;
            }
        }
    }
}
