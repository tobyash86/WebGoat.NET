using WebGoatCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebGoatCore.Data
{
    public class NorthwindContext : IdentityDbContext<IdentityUser>
    {
        public static void Initialize(IConfiguration configuration, IHostEnvironment env)
        {
            var execDirectory = configuration.GetValue(Constants.WEBGOAT_ROOT, env.ContentRootPath);
            var builder = new SqliteConnectionStringBuilder();
            builder.DataSource = Path.Combine(execDirectory, "NORTHWND.sqlite");
            ConnString = builder.ConnectionString;
            if(string.IsNullOrEmpty(ConnString))
            {
                throw new WebGoatCore.Exceptions.WebGoatStartupException("Cannot compute connection string to connect database!");
            }
        }

        public static string? ConnString;

        public static readonly LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
        });

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
