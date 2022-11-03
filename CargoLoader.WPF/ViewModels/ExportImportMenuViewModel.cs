using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public class ExportImportMenuViewModel : ViewModelBase
    {
        private string _exportFolder;
        public string ExportFolder
        {
            get => _exportFolder;
            set
            {
                _exportFolder = value;
                OnPropertyChanged(nameof(ExportFolder));
            }
        }

        private bool _productCheck;
        public bool ProductCheck
        {
            get { return _productCheck; }
            set
            {
                _productCheck = value;  
                OnPropertyChanged(nameof(ProductCheck));
            }
        }

        private bool _containerCheck;
        public bool ContainerCheck
        {
            get { return _containerCheck; }
            set
            {
                _containerCheck = value;
                OnPropertyChanged(nameof(ContainerCheck));
            }
        }

        public ExportImportMenuViewModel()
        {
        }

        

    }
}
