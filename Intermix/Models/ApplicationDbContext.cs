using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Intermix.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Database\Intermix.db");
            optionsBuilder.UseSqlite($"Data Source = {path}");
        }

        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Loans> Loans { get; set; }
    }

}
