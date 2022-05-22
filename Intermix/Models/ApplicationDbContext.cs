using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\Daniel\Desktop\Repozytorium GIT\InterMix\Intermix\Database\Intermix.db");
        }

        public DbSet<Users> Users { get; set; }
    }
}
