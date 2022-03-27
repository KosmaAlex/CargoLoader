using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Models
{
    public class Order : DomainObject
    {
        public string OrderNumber { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public List<Cargo> Cargo { get; set; } = null!;
    }
}
