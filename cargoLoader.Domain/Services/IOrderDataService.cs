using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Services
{
    public interface IOrderDataService : IDataService
    {
        Task<Order> GetByOrderNumber(string orderNumber);
    }
}
