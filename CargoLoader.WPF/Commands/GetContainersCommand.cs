using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Commands
{
    public class GetContainersCommand : ICommand
    {
        private readonly CSVContainerMapper _mapper = new CSVContainerMapper();
        private readonly IItemDataService<Container> _service;

        public event EventHandler? CanExecuteChanged;


        public GetContainersCommand(IItemDataService<Container> service)
        {
            _service = service;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            foreach(Container item in _mapper.Map())
            {
                _service.Create(item);
            }
        }
    }
}
