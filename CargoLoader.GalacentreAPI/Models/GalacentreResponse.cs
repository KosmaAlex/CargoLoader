using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI.Models
{
    public class GalacentreResponse
    {
        public GalacentreMetaObject Meta { get; set; }
        public List<GalacentreDataObject> Data { get; set; }
    }
}
