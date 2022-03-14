using Domains_Scraper.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Domains_Scraper.Entity_Framework_folder
{
    public class LibraryContext : DbContext
    {
        public DbSet<SemrushDomain> SemrushDomain { get; set; }
        //public DbSet<AhrefDomain> AhrefDomain { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Write);
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            optionsBuilder.UseMySql("server=localhost;Port=3306;database=library;user=root;password=bilel23051984", serverVersion);


        }
        public void Write(string s)
        {
            Debug.WriteLine(s);
        }
    }
}
