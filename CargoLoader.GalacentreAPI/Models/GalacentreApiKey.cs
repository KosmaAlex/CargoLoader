using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI.Models
{
    public class GalacentreApiKey
    {
        public string Key { get; }

        public GalacentreApiKey(string key)
        {
            Key = key;
        }
    }
}
