using Microsoft.EntityFrameworkCore;
using Models;

namespace RepositoryLayer
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Location> Locations {get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderLine> OrderLines {get;set;}
    
        public DbSet<Product> Products {get;set;}
        public DbSet<Inventory> Inventories {get;set;}

        public StoreDbContext() { }
        public StoreDbContext(DbContextOptions options) : base(options) { }



        protected override void OnConfiguring(DbContextOptionsBuilder options){
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StoreApplicationP1;Trusted_Connection=True;");
        }
    }
}