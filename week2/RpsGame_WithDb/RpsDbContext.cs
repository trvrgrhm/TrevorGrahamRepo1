using Microsoft.EntityFrameworkCore;

namespace RpsGame_WithDb
{
    public class RpsDbContext : DbContext
    {
        public DbSet<Player> players {get;set;}
        public DbSet<Round> rounds {get;set;}
        public DbSet<Match> matches {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options){
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RpsGame12142020;Trusted_Connection=True;");
        }
    }
}