using CargoLoader.Domain.Exceptions;
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
        private readonly Dictionary<string, Func<IQueryable<T>,IQueryable<T>>> _queries;


        public ItemDataService(CargoLoaderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
            _queries = new Dictionary<string, Func<IQueryable<T>, IQueryable<T>>>();
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
            decimal parameter, decimal minParameter = Constants.DefaultMinValue)
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

        public void QueryByHeight(decimal? height, decimal? minHeight)
        {
            if (_queries.ContainsKey(nameof(QueryByHeight)))
            {
                _queries.Remove(nameof(QueryByHeight));
            }

            if (height == null && minHeight == null)
            {                
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where height == null ? entity.Height >= minHeight
                                 : minHeight == null ? entity.Height <= height
                                 : entity.Height <= height && entity.Height >= minHeight
                                 select entity;

            _queries.Add(nameof(QueryByHeight), query);
        }

        public void QueryByIsContainer(bool? isContainer)
        {
            if (_queries.ContainsKey(nameof(QueryByIsContainer)))
            {
                _queries.Remove(nameof(QueryByIsContainer));
            }

            if (isContainer == null)
            {               
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where entity.IsContainer == isContainer
                                 select entity;
                                    
            _queries.Add(nameof(QueryByIsContainer), query);
        }

        public void QueryByIsFragile(bool? isFragile)
        {
            if (_queries.ContainsKey(nameof(QueryByIsFragile)))
            {
                _queries.Remove(nameof(QueryByIsFragile));
            }

            if (isFragile == null)
            {                
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;
            query = (context) => from entity in context
                                 where entity.IsFragile == isFragile
                                 select entity;
                        
            _queries.Add(nameof(QueryByIsFragile), query);
        }

        public void QueryByIsProp(bool? isProp)
        {
            if (_queries.ContainsKey(nameof(QueryByIsProp)))
            {
                _queries.Remove(nameof(QueryByIsProp));
            }

            if (isProp == null)
            {                
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;
            query = (context) => from entity in context
                                 where entity.IsProp == isProp
                                 select entity;

            _queries.Add(nameof(QueryByIsProp), query);
        }

        public void QueryByIsRotatable(bool? isRotatable)
        {
            if (_queries.ContainsKey(nameof(QueryByIsRotatable)))
            {
                _queries.Remove(nameof(QueryByIsRotatable));
            }

            if(isRotatable == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;
            query = (context) => from entity in context
                                 where entity.IsRotatable == isRotatable
                                 select entity;

            _queries.Add(nameof(QueryByIsRotatable), query);
        }

        public void QueryByLength(decimal? length, decimal? minLength)
        {
            if (_queries.ContainsKey(nameof(QueryByLength)))
            {
                _queries.Remove(nameof(QueryByLength));
            }

            if(minLength == null && length == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where length == null ? entity.Length >= minLength
                                 : minLength == null ? entity.Length <= length
                                 : entity.Length <= length && entity.Length >= minLength
                                 select entity;

            _queries.Add(nameof(QueryByLength), query);
        }

        public void QueryByMarking(string? marking)
        {
            if (_queries.ContainsKey(nameof(QueryByMarking)))
            {
                _queries.Remove(nameof(QueryByMarking));
            }

            if (marking == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where entity.Marking.Contains(marking)
                                 select entity;

            _queries.Add(nameof(QueryByMarking), query);
        }

        public void QueryByName(string? name)
        {
            if (_queries.ContainsKey(nameof(QueryByName)))
            {
                _queries.Remove(nameof(QueryByName));
            }

            if(name == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where entity.Name.Contains(name)
                                 select entity;

            _queries.Add(nameof(QueryByName), query);
        }

        public void QueryByVolume(decimal? volume, decimal? minVolume)
        {
            if (_queries.ContainsKey(nameof(QueryByVolume)))
            {
                _queries.Remove(nameof(QueryByVolume));
            }            

            if(volume == null && minVolume == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where volume == null ? entity.Volume >= minVolume
                                 : minVolume == null ? entity.Volume <= volume
                                 : entity.Volume <= volume && entity.Volume >= minVolume
                                 select entity;

            _queries.Add(nameof(QueryByVolume), query);
        }

        public void QueryByWeight(decimal? weight, decimal? minWeight)
        {
            if (_queries.ContainsKey(nameof(QueryByWeight)))
            {
                _queries.Remove(nameof(QueryByWeight));
            }

            if(weight == null && minWeight == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;

            query = (context) => from entity in context
                                 where weight == null ? entity.Weight >= minWeight 
                                 : minWeight == null ? entity.Weight <= weight
                                 : entity.Weight <= weight && entity.Weight >= minWeight
                                 select entity;

            _queries.Add(nameof(QueryByWeight), query);
        }

        public void QueryByWidth(decimal? width, decimal? minWidth)
        {
            if (_queries.ContainsKey(nameof(QueryByWidth)))
            {
                _queries.Remove(nameof(QueryByWidth));
            }

            if(width == null && minWidth == null)
            {
                return;
            }

            Func<IQueryable<T>, IQueryable<T>> query;
                        
            query = (context) => from entity in context
                                 where width == null ? entity.Width >= minWidth 
                                 : minWidth == null ? entity.Width <= width
                                 : entity.Width <= width && entity.Width >= minWidth
                                 select entity;

            _queries.Add(nameof(QueryByWidth), query);
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
                IQueryable<T> result = context.Set<T>()
                    .OrderBy(e => e.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return await result.ToListAsync();
            }
        }

        public async Task<(IEnumerable<T> filteredPage, int filteredPageCount)> ExecuteFilteringQuery(int pageNumber, int pageSize)
        {            
            if (_queries.Count == 0)
            {
                return (Enumerable.Empty<T>(), -1);
            }

            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IQueryable<T> query = context.Set<T>();

                foreach (var func in _queries.Values)
                {
                    query = func.Invoke(query);
                }

                IEnumerable<T> result = await query
                    .OrderBy(e => e.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();      
                
                int pageCount = (await query.CountAsync() + pageSize -1)/ pageSize;
                                
                return (result, pageCount);
            }
        }
    }
}
