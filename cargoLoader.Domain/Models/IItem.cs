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
        decimal Width { get; set; }
        decimal Height { get; set; }
        decimal Length { get; set; }
        decimal? Volume { get; set; }
        decimal Weight { get; set; }
        bool IsFragile { get; set; }
        bool IsRotatable { get; set; }
        bool IsProp { get; set; }
        bool IsContainer { get; set; }
    }
}
