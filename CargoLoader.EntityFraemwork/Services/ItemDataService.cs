﻿using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services
{    
    public class ItemDataService<T> : IItemDataService<T> where T : DomainObject, IItem
    {
        private readonly CargoLoaderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;


        public ItemDataService(CargoLoaderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task Create(T entity)
        {
            await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<T> Get(int id)
        {
            T result = await _nonQueryDataService.Get(id);
            return result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _nonQueryDataService.GetAll();
        }

        public async Task<IEnumerable<T>> GetByCustomProperty(string propertyName,
            double parameter, double minParameter = Constants.DefaultMinValue)
        {
            if(minParameter == Constants.DefaultMinValue)
            {
                minParameter = parameter;
            }

            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {                
                IEnumerable<T> result = await context.Set<T>()
                    .Where($"{propertyName} <= {parameter} && {propertyName} >= {minParameter}")
                    .ToListAsync();

                if(result.Count() == 0)
                {
                    throw new ItemNotFoundException(propertyName);
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByCustomProperty(string propertyName, bool parameter)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where($"{propertyName} == {parameter}")
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(propertyName);
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByHeight(double height, double minHeight = Constants.DefaultMinValue)
        {
            if(minHeight == Constants.DefaultMinValue)
            {
                minHeight = height;
            }
                        
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.Height <= height && e.Height >= minHeight)
                    .ToListAsync();

                if(result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(IItem.Height));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByIsContainer(bool isContainer)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.IsContainer == isContainer)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(isContainer));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByIsFragile(bool isFragile)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.IsFragile == isFragile)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(isFragile));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByIsProp(bool isProp)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.IsProp == isProp)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(isProp));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByIsRotatable(bool isRotatable)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.IsRotatable == isRotatable)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(isRotatable));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByLength(double length, double minLength = Constants.DefaultMinValue)
        {
            if(minLength == Constants.DefaultMinValue)
            {
                minLength = length;
            }

            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.Length <= length && e.Length >= minLength)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(length));
                }

                return result;
            }
        }

        public async Task<T> GetByMarking(string marking)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                T result = await context.Set<T>()
                    .FirstOrDefaultAsync(e => e.Marking == marking);

                if(result == null)
                {
                    throw new ItemNotFoundException(nameof(marking));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByName(string name)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.Name.Contains(name))
                    .ToListAsync();

                if(result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(name));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByVolume(double volume, double minVolume = Constants.DefaultMinValue)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                if(minVolume == Constants.DefaultMinValue)
                {
                    minVolume = volume;
                }

                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.Volume <= volume && e.Volume >= minVolume)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(volume));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByWeight(double weight, double minWeight = Constants.DefaultMinValue)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                
                if(minWeight == Constants.DefaultMinValue)
                {
                    minWeight = weight;
                }

                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.Weight <= weight && e.Weight >= minWeight)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(weight));
                }

                return result;
            }
        }

        public async Task<IEnumerable<T>> GetByWidth(double width, double minWidth = Constants.DefaultMinValue)
        {
            if(minWidth == Constants.DefaultMinValue)
            {
                minWidth = width;
            }

            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Where(e => e.Width <= width && e.Width >= minWidth)
                    .ToListAsync();

                if (result.Count() == 0)
                {
                    throw new ItemNotFoundException(nameof(width));
                }

                return result;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<int> GetTableCountAsync()
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                int result = await context.Set<T>().CountAsync();
                return result;
            }
        }
        public async Task<IEnumerable<T>> GetPageAsync(int page, int pageSize)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> result = await context.Set<T>()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return result;
            }
        }
    }
}
