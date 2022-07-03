using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork
{
    public class CargoLoaderDbContextFactory
    {        
        private readonly string _connectionString;
        public CargoLoaderDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CargoLoaderDbContext CreateContext()
        {
            DbContextOptionsBuilder<CargoLoaderDbContext> builder = new DbContextOptionsBuilder<CargoLoaderDbContext>();
            builder.UseSqlServer(_connectionString);

            return new CargoLoaderDbContext(builder.Options);
        }

    }
}
