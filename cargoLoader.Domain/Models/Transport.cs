using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Models
{
    public class Transport : DomainObject
    {
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public double VolumeCapacity { get; set; }
        public double WeightCapacity { get; set; }
    }
}
