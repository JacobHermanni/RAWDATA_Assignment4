using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    class NorthwindContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql(
                "server=localhost;" +
                "database=northwind;" +
                "uid=root;" 
                +
                /*"pwd=Arsenal1;"*/
                "pwd=root;"
            );

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasColumnName("categoryname");
            
            modelBuilder.Entity<Product>()
                .Property(x => x.Name)
                .HasColumnName("productname");

            modelBuilder.Entity<OrderDetails>()
                .Property(x => x.OrderId)
                .HasColumnName("orderid");
                                   
        }
    }
}
