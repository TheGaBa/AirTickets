using Microsoft.EntityFrameworkCore;
using Migrations.Models;

namespace Migrations
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Flights> Flights { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<ListOfUserThings> ListOfUserThings { get; set; }

        public DbSet<FutureFlight> FutureFlights { get; set; }

        public DbSet<EarlyFlight> EarlyFlights { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDatabase.db");
        }
    }
}