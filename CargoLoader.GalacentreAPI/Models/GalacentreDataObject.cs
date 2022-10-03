using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI.Models
{
    public class GalacentreDataObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Specifications { get; set; }
        public List<string> Props { get; set; }
        public string Image { get; set; }
    }
}
