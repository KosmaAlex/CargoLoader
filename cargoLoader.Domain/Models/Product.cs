using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Models
{
    public class Product : DomainObject, IItem 
    {
        public string Marking { get; set; }
        public string Name { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal? Volume { get; set; }
        public decimal Weight { get; set; }
        public bool IsFragile { get; set; }
        public bool IsRotatable { get; set; }
        public bool IsProp { get; set; }
        public bool IsContainer { get; set; } = false;
    }
}
