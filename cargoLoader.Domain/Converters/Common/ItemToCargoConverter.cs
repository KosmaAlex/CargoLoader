using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Converters.Common
{
    public class ItemToCargoConverter
    {
        public static Cargo Convert(IItem item)
        {
            Cargo cargo = new Cargo();
            cargo.Marking = item.Marking;
            cargo.Name = item.Name;
            cargo.Width = item.Width;
            cargo.Height = item.Height;
            cargo.Length = item.Length;
            cargo.Volume = item.Volume;
            cargo.Weight = item.Weight;
            cargo.IsFragile = item.IsFragile;
            cargo.IsRotatable = item.IsRotatable;
            cargo.IsProp = item.IsProp;
            cargo.IsContainer = item.IsContainer;

            return cargo;
        }
    }
}
