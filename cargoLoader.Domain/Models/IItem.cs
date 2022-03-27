using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Models
{
    public interface IItem
    {
        string Marking { get; set; }
        string Name { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        double Length { get; set; }
        double? Volume { get; set; }
        double Weight { get; set; }
        bool IsFragile { get; set; }
        bool IsRotatable { get; set; }
        bool IsProp { get; set; }
        bool IsContainer { get; set; }
    }
}
