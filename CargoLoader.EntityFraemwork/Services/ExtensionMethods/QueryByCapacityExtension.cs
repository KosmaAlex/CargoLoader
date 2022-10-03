using CargoLoader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.ExtensionMethods
{
    public static class QueryByCapacityExtension
    {
        public static void QueryByCapacity<T>(this IItemDataService<T> dataService,
            decimal? capacity, decimal? minCapacity)
        {
            string propertyName = "Capacity";
            dataService.QueryByCustomProperty<decimal?>(propertyName, capacity, minCapacity);            
        }
    }
}
