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
        //private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        //public CargoLoaderDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        //{
        //    _configureDbContext = o => o.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CargoLoader;Trusted_Connection=True;");
        //    //_configureDbContext = configureDbContext;
        //}

        //public CargoLoaderDbContext CreateContext()
        //{
        //    DbContextOptionsBuilder<CargoLoaderDbContext> options = new DbContextOptionsBuilder<CargoLoaderDbContext>();

        //    _configureDbContext(options);

        //    return new CargoLoaderDbContext(options.Options);
        //}

        private readonly DbContextOptions _options; 
        public CargoLoaderDbContextFactory()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CargoLoader;Trusted_Connection=True;");
            _options = builder.Options;
        }

        public CargoLoaderDbContext CreateContext()
        {
            return new CargoLoaderDbContext(_options);
        }

    }
}
