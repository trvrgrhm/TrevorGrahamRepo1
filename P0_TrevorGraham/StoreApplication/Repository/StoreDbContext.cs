using Microsoft.EntityFrameworkCore;
using StoreApplication.Models;

namespace StoreApplication.Repository
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Location> Locations {get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<Inventory> Inventories {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options){
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StoreApplicationP0;Trusted_Connection=True;");
        }
    }
}