﻿using CargoLoader.Domain.Comparers;
using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.Common
{
    public class NonQueryDataService : IDataService
    {
        private readonly CargoLoaderDbContextFactory _contextFactory;

        public NonQueryDataService(CargoLoaderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Create<T>(T entity) where T : DomainObject
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                try
                {
                    await context.Set<T>().AddAsync(entity);
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }    
            }
        }
        public async Task<bool> Delete<T>(int id) where T : DomainObject
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                T entity = await context.Set<T>()
                    .FirstOrDefaultAsync(e => e.Id == id);

                if(entity == null)
                {
                    throw new EntityDoesNotExistException(typeof(T).Name, nameof(entity.Id), id.ToString());
                }

                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get<T>(int id) where T : DomainObject
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                T entity = await context.Set<T>()
                    .FirstOrDefaultAsync(e => e.Id == id);

                if( entity == null)
                {
                    throw new EntityDoesNotExistException(typeof(T).Name, nameof(entity.Id), id.ToString());
                }

                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : DomainObject
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync<T>();
                return entities;
            }
        }

        public async Task<T> Update<T>(int id, T entity) where T : DomainObject
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                entity.Id = id;

                context.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public bool Contains<T>(T entity, IEqualityComparer<T> comparer) where T : DomainObject
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                return context.Set<T>().Contains(entity, comparer);
            }
        }
    }
}
