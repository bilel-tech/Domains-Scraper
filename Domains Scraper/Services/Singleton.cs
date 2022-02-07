using Domains_Scraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains_Scraper.Services
{
    public static class Singleton
    {
        public static List<SemrushDomain> SemrushDomains = new List<SemrushDomain>();
        public static List<string> Domains = new List<string>();
        public static string ConnectionString = "server=localhost;Port=3306;database=library;user=root;password=bilel23051984";

    }
}
