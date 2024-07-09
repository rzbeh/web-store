﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyEshop.Models;

namespace MyEshop.Data
{
    public class MyEshopContext:DbContext
    {
        public MyEshopContext(DbContextOptions<MyEshopContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDeatails> OrderDeatails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>()
                .HasKey(t => new {t.ProductId , t.CategoryId});

            //modelBuilder.Entity<Product>(
                
            //    p =>
            //    {
            //        p.HasKey(w => w.Id);
            //        p.OwnsOne<Item>(w => w.Item);
            //        p.HasOne<Item>(w => w.Item).WithOne(w => w.Product)
            //        .HasForeignKey<Item>(w => w.Id);
            //    }
            //    );

            modelBuilder.Entity<Item>(i =>
            {
                i.Property( w=> w.Price ).HasColumnType("Money");
                i.HasKey(w => w.Id);

            });
            #region Seed Data Category

            modelBuilder.Entity<Category>().HasData( new Category()
            {
                Id = 1,
                Name = "Admin",
                Description = "Admin Only"
            },

             new Category()
             {
                 Id = 2,
                 Name = "CoAdmin",
                 Description = "CoAdmin Only"
             },

              new Category()
              {
                  Id = 3,
                  Name = "Men Otfit",
                  Description = "trendy outfit for mens"
              },

               new Category()
               {
                   Id = 4,
                   Name = "Women Outfit",
                   Description = "Trendy outfit for womens"
               }

              


            );
            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Price = 854.0m,
                    QuantityInStock = 2
                },
            new Item()
            {
                Id = 2,
                Price = 854.0m,
                QuantityInStock =3
                },
            new Item()
            {
                Id = 3,
                Price = 854.0m,
                QuantityInStock =3
                },
            new Item()
            {
                Id = 4,
                Price = 854.0m,
                QuantityInStock =3
                });

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {

                    Id=1 ,
                    ItemId =1 ,
                    Name = "Box Fit Balenciaga T-Shirt",
                    Description = "New Realese Balenciaga Tshirt From Summer Collection"

                },
                 new Product()
                 {

                     Id =2 ,
                     ItemId =2 ,
                     Name = " Straight Baggy Balenciaga Pants",
                     Description = "New Realese Balenciaga Tshirt From Summer Collection"

                 },
                  new Product()
                  {

                      Id =3 ,
                      ItemId = 3,
                      Name = "Balenciaga Hat",
                      Description = "New Realese Balenciaga Hat From Summer Collection"

                  });

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryId = 1, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 1 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 1 },

                new CategoryToProduct() { CategoryId = 1, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 2 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 2 },

                new CategoryToProduct() { CategoryId = 1, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 2, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 3, ProductId = 3 },
                new CategoryToProduct() { CategoryId = 4, ProductId = 3 }
                 
                );

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
