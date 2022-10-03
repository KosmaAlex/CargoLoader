using CargoLoader.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Models
{
    public class Cargo : DomainObject, IItem
    {
        public string Marking { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal? Volume { get; set; }
        public decimal Weight { get; set; }
        public bool IsFragile { get; set; }
        public bool IsRotatable { get; set; }
        public bool IsProp { get; set; }
        public bool IsContainer { get; set; }
        public decimal? Capacity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public List<Cargo>? ContainedCargo { get; set; }
        public int? ContainerId { get; set; }
        public Cargo? Container { get; set; }

        [Image]
        public byte[]? Thumbnail { get; set; }
    }
}
