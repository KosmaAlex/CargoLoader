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
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double? Volume { get; set; }
        public double Weight { get; set; }
        public bool IsFragile { get; set; }
        public bool IsRotatable { get; set; }
        public bool IsProp { get; set; }
        public bool IsContainer { get; set; }
        public double? Capacity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public List<Cargo>? ContainedCargo { get; set; }
        public int? ContainerId { get; set; }
        public Cargo? Container { get; set; }
    }
}
