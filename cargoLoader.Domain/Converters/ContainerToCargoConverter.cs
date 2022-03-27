using CargoLoader.Domain.Converters.Common;
using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Converters
{
    public class ContainerToCargoConverter
    {
        public static Cargo Convert(Container container)
        {
            Cargo cargo = ItemToCargoConverter.Convert(container); 
            cargo.Capacity = container.Capacity;

            return cargo;   
        }
    }
}
