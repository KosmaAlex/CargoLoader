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
    public class OrderDataService : IOrderDataService
    {
        private readonly CargoLoaderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Order> _nonQueryDataService;

        public OrderDataService(CargoLoaderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Order>(contextFactory);
        }

        public async Task Create(Order entity)
        {
            using(CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                Order order = await context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == entity.OrderNumber);

                if(order != null)
                {
                    throw new OrderAlreadyExistException(order.Id, order.OrderNumber);
                }

                await context.Orders.AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Order> Get(int id)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                Order order = await context.Orders
                    .Include(o => o.Cargo)
                    .FirstOrDefaultAsync(o => o.Id == id);

                return order;
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                IEnumerable<Order> orders = await context.Orders
                    .Include(o => o.Cargo)
                    .ToListAsync();

                return orders;
            }
        }

        public async Task<Order> GetByOrderNumber(string orderNumber)
        {
            using (CargoLoaderDbContext context = _contextFactory.CreateContext())
            {
                Order order = await context.Orders
                    .Include(o => o.Cargo)
                    .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

                return order;
            }
        }

        public Task<Order> Update(int id, Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
