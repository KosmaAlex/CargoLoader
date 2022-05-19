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
            //TODO: find a difference between generic and non-generic optionsBuilder 
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(_connectionString);

            return new CargoLoaderDbContext(builder.Options);
        }

    }
}
