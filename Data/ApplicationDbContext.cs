using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechLife.Models;

namespace TechLife.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<SimService> SimServices { get; set; }
        public DbSet<SimServicesToDo> SimServiceToDos { get; set; }
        public DbSet<ShopStore> ShopStores { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierItem> SupplierItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<ProductView> ProductViews { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shop>().HasData(
                new Shop { ShopId = 1, Name = "Headphones", Category = "ACCESSORIES", Description = "Crisp, immersive sound experience. Choose from over-ear, on-ear, or in-ear styles for personalized comfort. Features include wireless options, noise cancellation, and convenient controls. Elevate your audio journey with our headphones.", ImgURL = "img/headphones.jpg", Price = 45, Quantity = 0 },
                new Shop { ShopId = 2, Name = "Cell Phone Charger", Category = "ACCESSORIES", Description = "Reliable power solutions for your devices. Fast-charging and durable phone chargers to keep you connected on the go. Choose from a variety of models tailored to your device's needs. Stay powered up, wherever you are", ImgURL = "img/cellphone-charger.jpg", Price = 10, Quantity = 0 },
                new Shop { ShopId = 3, Name = "Earphones", Category = "ACCESSORIES", Description = "Compact and powerful earphones for exceptional sound on the move. Enjoy immersive audio with comfortable in-ear designs. Choose from wired or wireless options with superior sound quality. Your perfect companion for music, calls, and more", ImgURL = "img/earphones.jpg", Price = 5, Quantity = 0 }
                );
            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartID = 1, CartName = "Headphones", CartPrice = 10, CartQuantity = 1, CartSubTotal = 10, ImgURL = "img/headphones.jpg", ShopId = 1 },
                new Cart { CartID = 2, CartName = "Cell Phone Charger", CartPrice = 20, CartQuantity = 1, CartSubTotal = 20, ImgURL = "img/headphones.jpg", ShopId = 2 },
                new Cart { CartID = 3, CartName = "Earphones", CartPrice = 5, CartQuantity = 1, CartSubTotal = 5, ImgURL = "img/earphones.jpg", ShopId = 3 }
                );
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceId = 1, CustName = "Hasan fakih", deviceModel = "Iphone 14", Location = "Jnoub, Kfarfila", message = "The screen is broken", ServiceType = "SmartPhone Repair" }
                );
            modelBuilder.Entity<SimService>().HasData(
                new SimService { SimServiceId = 1, SimServiceName = "4.5$ Recharging Card", PhoneNumber = " ", Amount = 0, Price = 4, SimType = "Mtc", ImgUrl = "~/img/4.5USDRecharge Voucher.jpg" },
                new SimService { SimServiceId = 2, SimServiceName = "7.5$ Recharging Card", PhoneNumber = " ", Amount = 0, Price = 7, SimType = "Mtc", ImgUrl = "~/img/1657112223214_image.jpg" },
                new SimService { SimServiceId = 3, SimServiceName = "4.5$ Recharging Card", PhoneNumber = " ", Amount = 0, Price = 4, SimType = "Alfa", ImgUrl = "~/img/Untitled-6.jpg" },
                new SimService { SimServiceId = 4, SimServiceName = "7.5$ Recharging Card", PhoneNumber = " ", Amount = 0, Price = 7, SimType = "Alfa", ImgUrl = "~/img/1fd2fc9d-e1ac-4c1e-9c07-1d78f99055cb.jpg" }
                );
            modelBuilder.Entity<ShopStore>()
                .HasOne(s => s.Stock)
                .WithOne(s => s.ShopStore)
                .HasForeignKey<Stock>(s => s.ShopStoreId);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supplier>()
       .HasMany(s => s.SupplierItems)
       .WithOne(si => si.Supplier)
       .HasForeignKey(si => si.SupplierId)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplierItem>()
                .HasOne(si => si.Supplier)
                .WithMany(s => s.SupplierItems)
                .HasForeignKey(si => si.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.SupplierItem)
                .WithMany(si => si.Stocks)
                .HasForeignKey(s => s.SupplierItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);
        }


    }
}