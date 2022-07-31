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
            decimal parameter, decimal minParameter = Constants.DefaultMinValue)
        {
            return await _itemDataService.GetByCustomProperty(propertyName, parameter, minParameter);
        }

        public async Task<IEnumerable<Cargo>> GetByCustomProperty(string propertyName, bool parameter)
        {
            return await _itemDataService.GetByCustomProperty(propertyName, parameter);
        }

        public void QueryByHeight(decimal? maxHeight, decimal? minHeight)
        {
            _itemDataService.QueryByHeight(maxHeight, minHeight);
        }

        public void QueryByIsContainer(bool? isContainer)
        {
            _itemDataService.QueryByIsContainer(isContainer);
        }

        public void QueryByIsFragile(bool? isFragile)
        {
            _itemDataService.QueryByIsFragile(isFragile);
        }

        public void QueryByIsProp(bool? isProp)
        {
            _itemDataService.QueryByIsProp(isProp);
        }

        public async void QueryByIsRotatable(bool? isRotatable)
        {
            _itemDataService.QueryByIsRotatable(isRotatable);
        }

        public void QueryByLength(decimal? maxLength, decimal? minLength)
        {
            _itemDataService.QueryByLength(maxLength, minLength);
        }

        public void QueryByMarking(string? marking)
        {
            _itemDataService.QueryByMarking(marking);
        }

        public void QueryByName(string? name)
        {
            _itemDataService.QueryByName(name);
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

        public void QueryByVolume(decimal? volume, decimal? minVolume)
        {
            _itemDataService.QueryByVolume(volume, minVolume);
        }

        public void QueryByWeight(decimal? weight, decimal? minWeight)
        {
            _itemDataService.QueryByWeight(weight, minWeight);
        }

        public void QueryByWidth(decimal? maxWidth, decimal? minWidth)
        {
            _itemDataService.QueryByWidth(maxWidth, minWidth);
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

        
        public Task<(IEnumerable<Cargo> filteredPage, int filteredPageCount)> ExecuteFilteringQuery(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
