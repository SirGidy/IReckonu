using IreckonuFileHandler.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace IreckonuFileHandler.Services.Persistence.Contexts
{
    public class AppDbContext : DbContext

    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPath> ProductPaths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            //builder.Entity<Product>().ToTable("Product");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().HasKey(p => p.Key);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd().HasValueGenerator<InMemoryIntegerValueGenerator<int>>();

            builder.Entity<Product>().Property(p => p.Key).IsRequired();
            builder.Entity<Product>().Property(p => p.Code).IsRequired();
            builder.Entity<Product>().Property(p => p.ColorCode).IsRequired();
            builder.Entity<Product>().Property(p => p.Description).IsRequired();
            builder.Entity<Product>().Property(p => p.Price).IsRequired();
            builder.Entity<Product>().Property(p => p.DiscountPrice) ;
            builder.Entity<Product>().Property(p => p.ExpectedDelivery).IsRequired(); ;
            builder.Entity<Product>().Property(p => p.Q1).IsRequired(); ;
            builder.Entity<Product>().Property(p => p.Size).IsRequired(); ;
            builder.Entity<Product>().Property(p => p.Color).IsRequired(); ;
            builder.Entity<Product>().Property(p => p.PathId).IsRequired(); ;





            builder.Entity<ProductPath>().HasKey(p => p.Id);
            builder.Entity<ProductPath>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd().HasValueGenerator<InMemoryIntegerValueGenerator<int>>();

            builder.Entity<ProductPath>().Property(p => p.JsonFilePath).IsRequired();
        





        }

    }
}
