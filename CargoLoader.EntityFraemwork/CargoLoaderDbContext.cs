using CargoLoader.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork
{
    public class CargoLoaderDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transport> Transports { get; set; }

        public CargoLoaderDbContext(DbContextOptions<CargoLoaderDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>().HasMany(c => c.ContainedCargo).WithOne(p => p.Container);
            modelBuilder.Entity<Cargo>().HasOne(c => c.Order).WithMany(o => o.Cargo);

            modelBuilder.Entity<Product>().HasIndex(p => p.Marking).IsUnique();
            modelBuilder.Entity<Container>().HasIndex(c => c.Marking).IsUnique();
            modelBuilder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();


            //TODO: change type from double to decimal. migrate new calculation
            modelBuilder.Entity<Product>().Property(p => p.Volume).HasComputedColumnSql(Constants.VolumeColumnCalc);
            modelBuilder.Entity<Container>().Property(c => c.Volume).HasComputedColumnSql(Constants.VolumeColumnCalc);
            modelBuilder.Entity<Cargo>().Property(c => c.Volume).HasComputedColumnSql(Constants.VolumeColumnCalc);

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
