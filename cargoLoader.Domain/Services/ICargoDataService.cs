using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Services
{
    public interface ICargoDataService : IItemDataService<Cargo>
    {
        Task<IEnumerable<Cargo>> GetByOrderId(int orderId);
        Task<IEnumerable<Cargo>> GetByContainedCargo(Cargo cargo); 
        Task<IEnumerable<Cargo>> GetByContainerId(int containerId);
    }
}
