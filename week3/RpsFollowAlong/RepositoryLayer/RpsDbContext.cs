using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class RpsDbContext : DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Round> rounds { get; set; }
        public DbSet<Match> matches { get; set; }

        public RpsDbContext() { }
        public RpsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RpsGameMVC;Trusted_Connection=True;");
            }
        }
    }
}
