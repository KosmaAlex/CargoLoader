﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.WPF.ViewModels
{
    public interface IFiltersViewModel
    {
        Type FiltersType { get; }
        IList<IFiltersViewModel> FiltersCollection { get; }
    }
}
