﻿using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.ExtensionMethods
{
    public static class GetByCapacityExtension
    {
        public static async Task<IEnumerable<T>> GetByCapacityAsync<T>(this IItemDataService<T> dataService,
            double capacity, double minCapacity = default)
        {
            string propertyName = "Capacity";

            IEnumerable<T> result = await dataService.GetByCustomProperty(propertyName, capacity, minCapacity);

            if(result == null)
            {
                throw new ItemNotFoundException(propertyName);
            }

            return result;
        }
    }
}
