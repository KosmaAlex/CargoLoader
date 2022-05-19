using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI.Services
{
    public class MappingService
    {
        private readonly GalacentreHttpClient _httpClient;

        public MappingService(GalacentreHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
