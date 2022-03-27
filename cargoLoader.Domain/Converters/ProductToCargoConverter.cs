using CargoLoader.Domain.Converters.Common;
using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Converters
{
    public class ProductToCargoConverter
    {
        public static Cargo Convert(Product product)
        {
            return ItemToCargoConverter.Convert(product);
        }
    }
}
