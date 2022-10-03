using CargoLoader.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Models
{
    public class Container : DomainObject, IItem
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
        public bool IsContainer { get; set; } = true;
        public decimal? Capacity { get; set; }

        [Image]
        public byte[]? Thumbnail { get; set; }
    }
}
