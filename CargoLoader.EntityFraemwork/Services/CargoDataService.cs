using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services
{
    public class CargoDataService : ICargoDataService
    {
        private readonly CargoLoaderDbContextFactory _contextFactory;
        private readonly ItemDataService<Cargo> _itemDataService;

        public CargoDataService(CargoLoaderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _itemDataService = new ItemDataService<Cargo>(contextFactory);
        }        

        public async Task Create(Cargo entity)
        {
            await _itemDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _itemDataService.Delete(id);
        }

        public async Task<Cargo> Get(int id)
        {
            return await _itemDataService.Get(id);
        }

        public async Task<IEnumerable<Cargo>> GetAll()
        {
            return await _itemDataService.GetAll();
        }

        public async Task<IEnumerable<Cargo>> GetByContainedCargo(Cargo containedCargo)
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<Cargo> result = await context.Cargo
                    .Where(e => e.IsContainer == true && e.ContainedCargo != null)
                    .Where(e => e.ContainedCargo.Contains(containedCargo))
                    .ToListAsync();

                if(result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(Cargo.ContainedCargo));
                }

                return result;
            }
        }

        public async Task<IEnumerable<Cargo>> GetByContainerId(int containerId)
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<Cargo> result = await context.Cargo
                    .Where(e => e.ContainerId != null)
                    .Where(e => e.ContainerId == containerId)
                    .ToListAsync();

                if(result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(Cargo.ContainerId));
                }

                return result;
            }
        }

        public async Task<IEnumerable<Cargo>> GetByCustomProperty(string propertyName,
            double parameter, double minParameter = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByCustomProperty(propertyName, parameter, minParameter);
        }

        public async Task<IEnumerable<Cargo>> GetByCustomProperty(string propertyName, bool parameter)
        {
            return await _itemDataService.GetByCustomProperty(propertyName, parameter);
        }

        public async Task<IEnumerable<Cargo>> GetByHeight(double maxHeight, double minHeight = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByHeight(maxHeight, minHeight);
        }

        public async Task<IEnumerable<Cargo>> GetByIsContainer(bool isContainer)
        {
            return await _itemDataService.GetByIsContainer(isContainer);
        }

        public async Task<IEnumerable<Cargo>> GetByIsFragile(bool isFragile)
        {
            return await _itemDataService.GetByIsFragile(isFragile);
        }

        public async Task<IEnumerable<Cargo>> GetByIsProp(bool isProp)
        {
            return await _itemDataService.GetByIsProp(isProp);
        }

        public async Task<IEnumerable<Cargo>> GetByIsRotatable(bool isRotatable)
        {
            return await _itemDataService.GetByIsRotatable(isRotatable);
        }

        public async Task<IEnumerable<Cargo>> GetByLength(double maxLength, double minLength = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByLength(maxLength, minLength);
        }

        public async Task<Cargo> GetByMarking(string marking)
        {
            return await _itemDataService.GetByMarking(marking);
        }

        public async Task<IEnumerable<Cargo>> GetByName(string name)
        {
            return await _itemDataService.GetByName(name);
        }

        public async Task<IEnumerable<Cargo>> GetByOrderId(int orderId)
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<Cargo> result = await context.Cargo
                    .Where(e => e.OrderId == orderId)
                    .ToListAsync();

                if(result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(Cargo.OrderId));
                }

                return result;
            }
        }

        public async Task<IEnumerable<Cargo>> GetByVolume(double volume, double minVolume = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByVolume(volume, minVolume);
        }

        public async Task<IEnumerable<Cargo>> GetByWeight(double weight, double minWeight = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByWeight(weight, minWeight);
        }

        public async Task<IEnumerable<Cargo>> GetByWidth(double maxWidth, double minWidth = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByWidth(maxWidth, minWidth);
        }

        public Task<IEnumerable<Cargo>> GetPageAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTableCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Cargo> Update(int id, Cargo entity)
        {
            return await _itemDataService.Update(id, entity);
        }
    }
}
