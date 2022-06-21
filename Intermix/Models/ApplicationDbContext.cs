using Microsoft.EntityFrameworkCore;

namespace Intermix.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //DO POPRAWY BO TO SYF KOD

            optionsBuilder.UseSqlite(@"Data Source = C:\Users\Daniel\Desktop\Repozytorium GIT\InterMix\Intermix\Database\Intermix.db");

        }

        public DbSet<Users> Users { get; set; }
    }
}
