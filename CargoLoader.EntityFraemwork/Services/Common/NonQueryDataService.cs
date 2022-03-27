using CargoLoader.Domain.Models;
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
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly CargoLoaderDbContextFactory _contextFactory;

        public NonQueryDataService(CargoLoaderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Create(T entity)
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
        public async Task<bool> Delete(int id)
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync<T>();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                entity.Id = id;

                context.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
