﻿using CargoLoader.GalacentreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CargoLoader.WPF.Commands
{
    public class GetDataToDbCommand : ICommand
    {
        private readonly GalacentreMappingService _galacentreMappingService;
        
        public event EventHandler? CanExecuteChanged;

        public GetDataToDbCommand(GalacentreMappingService galacentreMappingService)
        {
            _galacentreMappingService = galacentreMappingService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        //TODO: fix async void.
        public async void Execute(object? parameter)
        {
            await _galacentreMappingService.LoadDataToDb();
        }
    }
}
