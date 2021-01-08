using Microsoft.EntityFrameworkCore;
using Migrations.Models;

namespace Migrations
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDatabase.db");
        }
    }
}