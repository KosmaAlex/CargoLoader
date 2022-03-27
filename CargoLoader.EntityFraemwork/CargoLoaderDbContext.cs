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

        public CargoLoaderDbContext() { }
        public CargoLoaderDbContext(DbContextOptions options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CargoLoader;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>().HasMany(c => c.ContainedCargo).WithOne(p => p.Container);
            modelBuilder.Entity<Cargo>().HasOne(c => c.Order).WithMany(o => o.Cargo);

            modelBuilder.Entity<Product>().HasIndex(p => p.Marking).IsUnique();
            modelBuilder.Entity<Container>().HasIndex(c => c.Marking).IsUnique();
            modelBuilder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();

            modelBuilder.Entity<Product>().Property(p => p.Volume).HasComputedColumnSql("Length * Height * Width");
            modelBuilder.Entity<Container>().Property(c => c.Volume).HasComputedColumnSql("Length * Height * Width");
            modelBuilder.Entity<Cargo>().Property(c => c.Volume).HasComputedColumnSql("Length * Height * Width");

            #region hide
            //modelBuilder.Entity<Product>().HasMany(p => p.InContainer).WithMany(c => c.Products);
            //modelBuilder.Entity<Product>().HasMany(p => p.Orders).WithMany(o => o.Products);
            //modelBuilder.Entity<Container>().HasMany(c => c.Products).WithMany(p => p.InContainer);
            //modelBuilder.Entity<Container>().HasMany(c => c.Containers).WithMany(c => c.InContainer).UsingEntity(c =>
            //{
            //    c.ToTable("ContainerJoin");

            //});
            //modelBuilder.Entity<Container>().HasMany(c => c.Orders).WithMany(o => o.Containers);
            //modelBuilder.Entity<Order>().HasMany(o => o.Products).WithMany(p => p.Orders);
            //modelBuilder.Entity<Order>().HasMany(o => o.Containers).WithMany(c => c.Orders);
            //base.OnModelCreating(modelBuilder);
            #endregion
        }
    }
}
