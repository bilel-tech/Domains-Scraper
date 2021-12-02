using Domains_Scraper.Models;
using Microsoft.EntityFrameworkCore;

namespace Domains_Scraper.Entity_Framework_folder
{
    public class LibraryContext : DbContext
    {
        public DbSet<SemrushDomain> SemrushDomain { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            optionsBuilder.UseMySql("server=localhost;Port=3306;database=library;user=root;password=bilel23051984", serverVersion);
        }
    }
}
