using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Infrastructure
{
    public class NorthwindContext : IdentityDbContext<IdentityUser>
    {
        public static string ConnString =$"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Environment.CurrentDirectory}\\NORTHWND.MDF;Integrated Security=True;";
        
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {

        }

        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<BlogResponse> BlogResponses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetail>().HasKey(a => new { a.ProductId, a.OrderId });
        }
    }
}
