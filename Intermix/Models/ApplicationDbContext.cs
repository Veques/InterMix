using Microsoft.EntityFrameworkCore;
using System;

namespace Intermix.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(@"Data Source = Database\Intermix.db");

        }
        
        public DbSet<Users> Users { get; set; }
    }

    public class ApplicationDb : IDisposable
    {

        private ApplicationDbContext dbc = new();

        public ApplicationDb()
        {
            dbc.Database.EnsureCreated();
        }

        public void Dispose()
        {
            dbc.Dispose();
        }
    }
}
